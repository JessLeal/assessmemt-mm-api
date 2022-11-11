using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Errors;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using static MoneyMeAPI.Constants.ProductTypeEnum;

namespace MoneyMeAPI.Controllers
{
    public class RepaymentsController : BaseAPIController
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IRepaymentRepository _repaymentRepository;
        private readonly IRepaymentsProviderFactory _repaymentsProviderFactory;
        private readonly IRepaymentValidator _repaymentValidator;
        private readonly IMapper _mapper;
        public RepaymentsController(ILoanRepository loanRepository,
            IRepaymentRepository repaymentRepository,
            IRepaymentsProviderFactory repaymentsProviderFactory,
            IRepaymentValidator repaymentValidator,
            IMapper mapper)
        {
            _loanRepository = loanRepository;
            _repaymentRepository = repaymentRepository;
            _repaymentsProviderFactory = repaymentsProviderFactory;
            _repaymentValidator = repaymentValidator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("repayments/{accountId}")]
        public async Task<ActionResult<RepaymentsDto>> GetRepaymentByAccountId([FromRoute] Guid accountId)
        {
            try
            {
                //Check if a loan and repayment is present for the given user
                var associatedLoan = await _loanRepository.GetLoanByAccountIdAsync(accountId);
                var associatedRepayment = await _repaymentRepository.GetRepaymentByAccountIdASync(accountId);

                //Return 404 if either loan or repayment is not present
                if (associatedLoan == null || associatedRepayment == null)
                {
                    return NotFound(new APIException(404, "Account not found"));
                }

                //Convert the loan and repayment to their respective DTOs then return to user
                var loanDto = _mapper.Map<LoanDto>(associatedLoan);
                var totalRepayment = loanDto.Term * associatedRepayment.Amount;

                var repaymentResult = new RepaymentsDto()
                {
                    Amount = associatedRepayment.Amount,
                    EstablishmentFee = associatedRepayment.EstablishmentFee,
                    Interest = associatedRepayment.Interest,
                    TotalRepayment = totalRepayment,
                    Loan = loanDto
                };

                return Ok(repaymentResult);

            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("repayments/{accountId}")]
        public async Task<ActionResult<RepaymentsDto>> CalculateRepaymentByAccountId(
            [FromRoute] Guid accountId,
            [FromBody] LoanDto loanDto)
        {
            try
            {
                //Get the repayment calculator service based on the product type
                var repaymentFactory = _repaymentsProviderFactory.GetProviderBasedOnType(
                    (ProductType)Enum.Parse(typeof(ProductType),
                    loanDto.Product));

                //Calculate repayment
                var repaymentsResult = repaymentFactory.CalculateRepayments(loanDto);

                //Return the repayment calculations if isSaveRepayment value is false
                //This will enable the user to recalcualate the repayments without saving to the database 
                //This will be used in the loan edit functionality in the UI
                if (!loanDto.IsSaveRepayment)
                {
                    return Ok(repaymentsResult);
                }

                //Validate if values are blacklisted and return BadRequest if they are
                var isBlacklisted = await _repaymentValidator.CheckRepaymentInputValidity(loanDto);

                if (isBlacklisted.mobile) return BadRequest(new APIException(400, "Mobile number is not valid"));
                if (isBlacklisted.domain) return BadRequest(new APIException(400, "Email address is not valid"));

                //Get associated loan based on account
                var associatedLoan = await _loanRepository.GetLoanByAccountIdAsync(accountId);
                if (associatedLoan == null) return NotFound(new APIException(404,"Account not found"));

                //Check if repayment already exist for the account
                var associatedRepayment = await _repaymentRepository.GetRepaymentByAccountIdASync(accountId);
                if (associatedRepayment != null) return BadRequest(new APIException(400, "Repayment quote is already available for this account"));

                _mapper.Map(loanDto, associatedLoan);

                _loanRepository.UpdateLoan(associatedLoan);

                var repaymentModel = _mapper.Map<RepaymentsModel>(repaymentsResult);
                repaymentModel.Account = associatedLoan.Account;
                _repaymentRepository.CreateRepayment(repaymentModel);

                //Save repayment and loan values
                if (await _repaymentRepository.SaveAllAsync())
                {
                    return Ok(repaymentsResult);
                }

                return BadRequest();
            }

            catch
            {
                throw;
            }
        }
    }
}
