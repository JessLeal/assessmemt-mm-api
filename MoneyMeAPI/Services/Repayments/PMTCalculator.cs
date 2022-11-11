using MoneyMeAPI.Interfaces;

namespace MoneyMeAPI.Services.Repayments
{
    public class PMTCalculator : IPMTCalculator
    {
        public PMTCalculator()
        {
        }

        public decimal CalculateRepayment(double rate, int term, decimal presentValue)
        {
            //Return ArgumentException if term is less than or equal to zero
            //since this can cause division be zero or negative repayments
            if (term <= 0) throw new ArgumentException("Term should not be less than or equal to 0");

            //Convert annual rate to monthly
            var monthlyRate = rate / 12;
            decimal repaymentPerPeriod;

            //Check if rate is zero and calculate the repayment period
            //This will prevent division be zero
            if (rate == 0)
            {
                repaymentPerPeriod = (presentValue / term);
                return repaymentPerPeriod;
            }

            //Calculate repayment based on ammortization formula
            repaymentPerPeriod = presentValue * (decimal)(monthlyRate * Math.Pow(1 + monthlyRate, term) / (Math.Pow(1 + monthlyRate, term) - 1));

            return repaymentPerPeriod;
        }
    }
}
