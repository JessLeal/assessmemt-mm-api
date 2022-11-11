using Microsoft.Extensions.Configuration;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using MoneyMeAPI.Services.Repayments;
using MoneyMeAPI.Services.URLGenerator;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeTests.Services.Repayments
{
    public class PMTCalculatorTest
    {
        private readonly IPMTCalculator _sut;

        public PMTCalculatorTest()
        {
            _sut = new PMTCalculator();
        }

        [Theory]
        [InlineData("120", 0, 5, 600)]
        [InlineData("122.108142820078200", 0.07, 5, 600)]
        public void CalculateRepayments_ShouldComputeProperly_WhenInputsAreValid(
            String expected, double rate, int term, decimal presentValue)
        {
            //Arrange
            var expectedDecimal = Convert.ToDecimal(expected);

            //Act
            var repayments = _sut.CalculateRepayment(rate, term, presentValue);


            //Assert
            Assert.Equal(expectedDecimal, repayments);

        }
    }
}
