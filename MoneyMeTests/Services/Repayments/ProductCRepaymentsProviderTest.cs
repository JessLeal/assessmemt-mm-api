

using Microsoft.Extensions.Configuration;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using MoneyMeAPI.Services.Repayments;
using MoneyMeAPI.Services.URLGenerator;
using Moq;

namespace MoneyMeTests.Services.UrlGenerator
{
    public class ProductCRepaymentsProviderTest
    {
        private readonly IRepaymentsCalculatorProvider _sut;

        private readonly Mock<IPMTCalculator> _pmtCalculatorMock = new Mock<IPMTCalculator>();



        public ProductCRepaymentsProviderTest()
        {
           _sut = new ProductCRepaymentProvider(_pmtCalculatorMock.Object);
        }

        [Fact]
        public void ProductC_ShouldCallPMTCalculatorOnce_WhenCalled()
        {
            decimal repaymentAmount = 1211.96923145468M;
            //Arrange
            _pmtCalculatorMock.Setup(x => x.CalculateRepayment(0.08, It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(repaymentAmount);

            var loan = new LoanDto
            {
                Amount = 15000,
                Term = 60,
            };

            var expected = new RepaymentsDto()
            {
                Amount = (decimal)repaymentAmount,
                EstablishmentFee = 300,
            };

            //Acct
            var repayments = _sut.CalculateRepayments(loan);



            //Assert
            _pmtCalculatorMock.Verify(x => x.CalculateRepayment(It.IsAny<double>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Once);
            Assert.Equal(expected.Amount, repayments.Amount);
            Assert.Equal(expected.EstablishmentFee, repayments.EstablishmentFee);
        }
    }
}