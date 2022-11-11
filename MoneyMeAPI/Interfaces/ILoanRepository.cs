using MoneyMeAPI.DTOs;
using MoneyMeAPI.Model;

namespace MoneyMeAPI.Interfaces
{
    public interface ILoanRepository
    {
        Task<bool> SaveAllAsync();
        Task<LoanModel> GetLoanByAccountIdAsync(Guid id);
        Task<LoanModel> GetLoanByParameterAsync(AccountSearchParams userParams);
        void CreateLoan(LoanModel loanModel);
        void UpdateLoan(LoanModel loanModel);
    }
}
