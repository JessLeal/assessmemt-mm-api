

using Microsoft.Extensions.Configuration;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using MoneyMeAPI.Services.Repayments;
using MoneyMeAPI.Services.URLGenerator;
using Moq;

namespace MoneyMeTests.Services.UrlGenerator
{
    public class ProductBRepaymentsProviderTest
    {
        private readonly IRepaymentsCalculatorProvider _sut;

        private readonly Mock<IPMTCalculator> _pmtCalculatorMock = new Mock<IPMTCalculator>();



        public ProductBRepaymentsProviderTest()
        {
           _sut = new ProductBRepaymentProvider(_pmtCalculatorMock.Object);
        }

        [Fact]
        public void ProductB_ShouldCallPMTCalculatorTwice_WhenCalled()
        {
            decimal repaymentAmountWithoutInterest = 255M;
            decimal repaymentAmountWithInterest = 308.3078155481276550M;
            decimal actualRepayment = 306.5308883631900665M;

            //Arrange
            _pmtCalculatorMock.Setup(x => x.CalculateRepayment(0, It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(repaymentAmountWithoutInterest);
            _pmtCalculatorMock.Setup(x => x.CalculateRepayment(0.08, It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(repaymentAmountWithInterest);

            var loan = new LoanDto
            {
                Amount = 15000,
                Term = 60,
            };

            var expected = new RepaymentsDto()
            {
                Amount = actualRepayment,
                EstablishmentFee = 300,
            };

            //Acct
            var repayments = _sut.CalculateRepayments(loan);



            //Assert
            _pmtCalculatorMock.Verify(x => x.CalculateRepayment(It.IsAny<double>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Exactly(2));
            Assert.Equal(expected.Amount, repayments.Amount);
            Assert.Equal(expected.EstablishmentFee, repayments.EstablishmentFee);
        }
    }
}