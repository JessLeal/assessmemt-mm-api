using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;

namespace MoneyMeAPI.Services.Repayments
{
    public class ProductARepaymentProvider : IRepaymentsCalculatorProvider
    {
        IPMTCalculator _pmtCalculator;
        public ProductARepaymentProvider(IPMTCalculator pmtCalculator)
        {
            _pmtCalculator = pmtCalculator;
        }

        public RepaymentsDto CalculateRepayments(LoanDto loan)
        {
            //Set rate to zero for Product A
            double rate = 0;
            decimal establishmentFee = 300;
            decimal totalLoanAmount = (decimal)(loan.Amount) + establishmentFee;

            //Calculate repayment
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
