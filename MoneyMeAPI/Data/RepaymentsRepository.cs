using Microsoft.EntityFrameworkCore;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;

namespace MoneyMeAPI.Data
{
    public class RepaymentsRepository : IRepaymentRepository
    {
        private readonly DataContext _context;
        public RepaymentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RepaymentsModel> GetRepaymentByAccountIdASync(Guid id)
        {
            return await _context.Repayments
                .Where(l => l.AccountId == id)
                .SingleOrDefaultAsync();
        }

        public void CreateRepayment(RepaymentsModel repayment)
        {
            _context.Repayments.Add(repayment);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
