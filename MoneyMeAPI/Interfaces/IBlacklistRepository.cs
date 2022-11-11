using MoneyMeAPI.Model;

namespace MoneyMeAPI.Interfaces
{
    public interface IBlacklistRepository
    {
        Task<IEnumerable<BlacklistModel>> GetAllBlacklistedValuesAsync();
    }
}