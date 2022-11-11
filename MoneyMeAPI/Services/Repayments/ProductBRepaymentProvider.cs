using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;

namespace MoneyMeAPI.Services.Repayments
{
    public class ProductBRepaymentProvider : IRepaymentsCalculatorProvider
    {
        private readonly IPMTCalculator _pmtCalculator;
        public ProductBRepaymentProvider(IPMTCalculator pmtCalculator)
        {
            _pmtCalculator = pmtCalculator;
        }
        public RepaymentsDto CalculateRepayments(LoanDto loan)
        {
            //Set rate to zero for the 1st 2 months for product B
            double interestFreeRate = 0;
            int termFreeOfInterest = 2;

            //Set rate for product B for the rest of the loan duration
            //Interest rate value is based on the product and is assumed to be 0.08 in this instance
            double rate = 0.08;
            decimal establishmentFee = 300;
            decimal totalLoanAmount = (decimal)(loan.Amount) + establishmentFee;

            //Calculate repayment for the 1st 2 months
            var interestFreeRepaymentAmount = _pmtCalculator.CalculateRepayment(interestFreeRate, loan.Term, totalLoanAmount);

            //Calculate the repayment amount for the rest of the loan with the 1st 2 month payment removed
            var newTotalAmount = totalLoanAmount - (interestFreeRepaymentAmount * termFreeOfInterest);
            var repaymentAmountWithInterest = _pmtCalculator.CalculateRepayment(rate, (loan.Term - termFreeOfInterest), newTotalAmount);

            //Calculate the weighted average of the repayment amount
            var repaymentAmount = ((termFreeOfInterest) * (interestFreeRepaymentAmount) + (loan.Term - termFreeOfInterest) * (repaymentAmountWithInterest)) / loan.Term;

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
