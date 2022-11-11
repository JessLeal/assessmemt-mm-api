using Microsoft.EntityFrameworkCore;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;

namespace MoneyMeAPI.Data
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DataContext _context;
        public LoanRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<LoanModel> GetLoanByAccountIdAsync(Guid id)
        {
            return await _context.Loans
                .Where(l => l.AccountId == id)
                .Include(l => l.Account)
                .SingleOrDefaultAsync();
        }

        public async Task<LoanModel> GetLoanByParameterAsync(AccountSearchParams accountSearchParams)
        {
            return await _context.Loans
               .Where(l => l.Account.FirstName == accountSearchParams.FirstName)
               .Where(l => l.Account.LastName == accountSearchParams.LastName)
               .Where(l => l.Account.DateOfBirth == accountSearchParams.DateOfBirth)
               .Include(l => l.Account)
               .SingleOrDefaultAsync();
        }

        public void CreateLoan(LoanModel loan)
        {
           _context.Loans.Add(loan);
        }

        public void UpdateLoan(LoanModel loan)
        {
            _context.Loans.Update(loan);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
