using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMeAPI.Constants;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Errors;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;

namespace MoneyMeAPI.Controllers
{
    public class LoanController : BaseAPIController
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly IURLGeneratorService _urlGeneratorService;
        public LoanController(ILoanRepository loanRepository, IMapper mapper, IURLGeneratorService urlGeneratorService)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _urlGeneratorService = urlGeneratorService;
        }

        [HttpGet]
        [Route("loan/{accountId}")]
        public async Task<ActionResult<LoanDto>> GetLoanByAccountId([FromRouteAttribute] Guid accountId)
        {
            try
            {
                var loanResult = await _loanRepository.GetLoanByAccountIdAsync(accountId);

                if (loanResult == null)
                {
                    return NotFound(new APIException(404, "Account not found"));
                }

                var loanDto = _mapper.Map<LoanDto>(loanResult);
                return Ok(loanDto);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("loan")]
        public async Task<ActionResult<RedirectURLDTO>> CreateLoan(CreateLoanDto createLoanDto)
        {
            try
            {
                var searchParams = new AccountSearchParams()
                {
                    FirstName = createLoanDto.FirstName,
                    LastName = createLoanDto.LastName,
                    DateOfBirth = createLoanDto.DateOfBirth
                };

                //Get associated loan based on parameter
                var loanResult = await _loanRepository.GetLoanByParameterAsync(searchParams);

                //If loan does not exist then create the loan eslse update the loan
                if (loanResult == null)
                {
                    loanResult = _mapper.Map<LoanModel>(createLoanDto);
                    _loanRepository.CreateLoan(loanResult);
                }
                else
                {
                    _mapper.Map(createLoanDto, loanResult);
                    _loanRepository.UpdateLoan(loanResult);
                }

                //Save the changes to the loan context then generate the redirect url
                if (await _loanRepository.SaveAllAsync())
                {
                    var redirectUrl = _urlGeneratorService.generateRedirectURL(loanResult.Account);
                    return Ok(redirectUrl);
                }

                return BadRequest(new APIException(400, "Failed to create loan"));
            }
            catch
            {
                throw;
            }
        }
    }
}
