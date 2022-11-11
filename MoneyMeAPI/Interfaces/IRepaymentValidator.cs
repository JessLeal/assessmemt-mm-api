using MoneyMeAPI.DTOs;
using MoneyMeAPI.Errors;

namespace MoneyMeAPI.Interfaces
{
    public interface IRepaymentValidator
    {
        Task<RepaymentValidationError> CheckRepaymentInputValidity(LoanDto loan);
    }
}