using MoneyMeAPI.DTOs;

namespace MoneyMeAPI.Interfaces
{
    public interface IRepaymentsCalculatorProvider
    {
        public RepaymentsDto CalculateRepayments(LoanDto loan);
    }
}
