using MoneyMeAPI.Constants;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Errors;
using MoneyMeAPI.Interfaces;

namespace MoneyMeAPI.Services.Repayments
{
    public class RepaymentValidator : IRepaymentValidator
    {
        private readonly IBlacklistRepository _blacklistRepository;
        public RepaymentValidator(IBlacklistRepository blacklistRepository)
        {
            _blacklistRepository = blacklistRepository;
        }

        public async Task<RepaymentValidationError> CheckRepaymentInputValidity(LoanDto loan)
        {
            //Get email domain
            var emailSplit = loan.Email.Split('@');
            var emailDomain = emailSplit[emailSplit.Length - 1];

            //Check if email or mobile is in the blacklist
            var blacklistedValues = await _blacklistRepository.GetAllBlacklistedValuesAsync();
            var blackListedMobile = blacklistedValues.Where(b => b.type == DefaultValues.MOBILE && b.value == loan.Mobile);
            var blackListedDomain = blacklistedValues.Where(b => b.type == DefaultValues.DOMAIN && b.value == emailDomain);

            return new RepaymentValidationError()
            {
                mobile = blackListedMobile.Count() > 0,
                domain = blackListedDomain.Count() > 0
            };

        }
    }
}
