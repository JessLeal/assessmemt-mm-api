using System.Diagnostics.CodeAnalysis;

namespace MoneyMeAPI.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CalculatedRepaymentsDto
    {
        public decimal Amount { get; set; }
        public decimal EstablishmentFee { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalRepayment { get; set; }
    }

    public class RepaymentsDto : CalculatedRepaymentsDto
    {
        public LoanDto Loan { get; set; }
    }

}
