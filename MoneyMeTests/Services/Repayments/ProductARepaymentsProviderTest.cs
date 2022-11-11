

using Microsoft.Extensions.Configuration;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using MoneyMeAPI.Services.Repayments;
using MoneyMeAPI.Services.URLGenerator;
using Moq;

namespace MoneyMeTests.Services.UrlGenerator
{
    public class ProductARepaymentsProviderTest
    {
        private readonly IRepaymentsCalculatorProvider _sut;

        private readonly Mock<IPMTCalculator> _pmtCalculatorMock = new Mock<IPMTCalculator>();



        public ProductARepaymentsProviderTest()
        {
           _sut = new ProductARepaymentProvider(_pmtCalculatorMock.Object);
        }

        [Fact]
        public void ProductA_ShouldCallPMTCalculatorOnce_WhenCalled()
        {
            //Arrange
            var amount = 2200;
            var establishmentFee = 300;
            var term = 2;

           _pmtCalculatorMock.Setup(x => x.CalculateRepayment(0, It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns((amount+establishmentFee)/term);

            var loan = new LoanDto
            {
                Amount = amount,
                Term = term,
            };

            var expected = new RepaymentsDto()
            {
                Amount = 1250,
                EstablishmentFee = 300,
                Interest = 0
            };

            //Acct
            var repayments = _sut.CalculateRepayments(loan);



            //Assert
            _pmtCalculatorMock.Verify(x => x.CalculateRepayment(It.IsAny<double>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Once);
            Assert.Equal(expected.Amount, repayments.Amount);
            Assert.Equal(expected.EstablishmentFee, repayments.EstablishmentFee);
            Assert.Equal(expected.Interest, repayments.Interest);
        }
    }
}