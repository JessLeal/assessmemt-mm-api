using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MoneyMeAPI.Model
{
    [ExcludeFromCodeCoverage]
    [Table("Loans")]
    public class LoanModel
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public int Term { get; set; }
        public string Product { get; set; }
        public Guid AccountId { get; set; }
        public AccountModel Account { get; set; }
    }
}
