using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;

namespace MoneyMeAPI.Services.Repayments
{
    public class ProductCRepaymentProvider : IRepaymentsCalculatorProvider
    {
        private readonly IPMTCalculator _pmtCalculator;
        public ProductCRepaymentProvider(IPMTCalculator pmtCalculator)
        {
            _pmtCalculator = pmtCalculator;
        }
        public RepaymentsDto CalculateRepayments(LoanDto loan)
        {
            //Set rate for product C
            //Interest rate value is based on the product and is assumed to be 0.08 in this instance
            double rate = 0.08;
            decimal establishmentFee = 300;
            decimal totalLoanAmount = (decimal)(loan.Amount) + establishmentFee;

            //Calculate repayments
            var repaymentAmount = _pmtCalculator.CalculateRepayment(rate, loan.Term, totalLoanAmount);
            var totalInterest = (repaymentAmount * loan.Term) - totalLoanAmount;
            var totalRepayment = loan.Term * repaymentAmount;

            var repaymentResult = new RepaymentsDto()
            {
                Amount = (decimal)repaymentAmount,
                EstablishmentFee = establishmentFee,
                Interest = totalInterest,
                TotalRepayment = totalRepayment,
                Loan = loan
            };

            return repaymentResult;
        }
    }
}
