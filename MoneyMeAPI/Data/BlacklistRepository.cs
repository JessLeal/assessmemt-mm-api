using Microsoft.EntityFrameworkCore;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;

namespace MoneyMeAPI.Data
{
    public class BlacklistRepository : IBlacklistRepository
    {
        private readonly DataContext _context;
        public BlacklistRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlacklistModel>> GetAllBlacklistedValuesAsync()
        {
            return await _context.Blacklists
                .ToListAsync();
        }
    }
}
