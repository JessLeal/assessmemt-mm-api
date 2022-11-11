using System.Diagnostics.CodeAnalysis;

namespace MoneyMeAPI.DTOs
{
    [ExcludeFromCodeCoverage]
    public class AccountSearchParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
