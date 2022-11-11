namespace MoneyMeAPI.Interfaces
{
    public interface IPMTCalculator
    {
        decimal CalculateRepayment(double rate, int term, decimal presentValue);
    }
}