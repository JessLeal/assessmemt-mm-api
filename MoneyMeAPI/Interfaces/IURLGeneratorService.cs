using MoneyMeAPI.Model;

namespace MoneyMeAPI.Interfaces
{
    public interface IURLGeneratorService
    {
        string generateRedirectURL(AccountModel account);
    }
}
