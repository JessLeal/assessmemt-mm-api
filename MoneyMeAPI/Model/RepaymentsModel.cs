using System.Diagnostics.CodeAnalysis;

namespace MoneyMeAPI.Model
{
    [ExcludeFromCodeCoverage]
    public class RepaymentsModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal EstablishmentFee { get; set; }
        public decimal Interest { get; set; }
        public Guid AccountId { get; set; }
        public AccountModel Account { get; set; }
    }
}
