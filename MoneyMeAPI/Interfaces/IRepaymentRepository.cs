using MoneyMeAPI.DTOs;
using MoneyMeAPI.Model;

namespace MoneyMeAPI.Interfaces
{
    public interface IRepaymentRepository
    {
        Task<bool> SaveAllAsync();
        
        void CreateRepayment(RepaymentsModel repaymentsModel);

        Task<RepaymentsModel> GetRepaymentByAccountIdASync(Guid id);

    }
}
