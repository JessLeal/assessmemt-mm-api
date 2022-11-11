

using Microsoft.Extensions.Configuration;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using MoneyMeAPI.Services.URLGenerator;
using Moq;

namespace MoneyMeTests.Services.UrlGenerator
{
    public class URLGeneratorServiceTest
    {
        private readonly IURLGeneratorService _sut;

        private readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        private readonly Mock<IConfigurationSection> _configurationSectionMock = new Mock<IConfigurationSection>();



        public URLGeneratorServiceTest()
        {
            _configurationSectionMock
               .Setup(x => x.Value)
               .Returns("http://somesUrl");

            _configurationMock
               .Setup(x => x.GetSection("RedirectUrlBase"))
               .Returns(_configurationSectionMock.Object);

            _sut = new URLGeneratorService(_configurationMock.Object);
        }

        [Fact]
        public void GenerateRedirectURL_ShouldBeGenerated_WhenAccountIsPresent()
        {
            //Arrange
            var baseUrl = "http://somesUrl";

            var account = new AccountModel
            {
                Id = new Guid()
            };

            //Acct
            var redirectUrl = _sut.generateRedirectURL(account);


            //Assert
            Assert.Equal($"{baseUrl}/{account.Id}", redirectUrl);
        }
    }
}