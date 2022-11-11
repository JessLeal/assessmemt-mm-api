using System.Diagnostics.CodeAnalysis;



namespace MoneyMeAPI.DTOs
{
    [ExcludeFromCodeCoverage]
    public class AccountDto
    {
        public string Title { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
