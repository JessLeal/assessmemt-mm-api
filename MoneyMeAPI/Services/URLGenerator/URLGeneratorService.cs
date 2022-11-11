using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using System.Text;

namespace MoneyMeAPI.Services.URLGenerator
{
    public class URLGeneratorService : IURLGeneratorService
    {
        private readonly string _redirectUrlBase;

        public URLGeneratorService(IConfiguration config)
        {
            _redirectUrlBase = config.GetValue<string>("RedirectUrlBase");
        }

        public string generateRedirectURL(AccountModel account)
        {
            //Generate redirect url based on the account id
            //Redirect url base is the frontend url
            return $"{_redirectUrlBase}/{account.Id}";
        }
    }
}
