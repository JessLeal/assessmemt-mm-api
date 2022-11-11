

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MoneyMeAPI.Controllers;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Model;
using Moq;
using System;
using System.Net;

namespace MoneyMeTests.Services.UrlGenerator
{
    public class RepaymentControllerTest
    {
        private readonly RepaymentsController _sut;
        private readonly Mock<ILoanRepository> _loanRepositoryMock = new Mock<ILoanRepository>();
        private readonly Mock<IRepaymentRepository> _repaymentsRepositoryMock = new Mock<IRepaymentRepository>();
        private readonly Mock<IRepaymentsProviderFactory> _repaymentsProviderFactoryMock = new Mock<IRepaymentsProviderFactory>();
        private readonly Mock<IRepaymentValidator> _repaymentValidatorMock = new Mock<IRepaymentValidator>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public RepaymentControllerTest()
        {
            _sut = new RepaymentsController(
                _loanRepositoryMock.Object, 
                _repaymentsRepositoryMock.Object,
                _repaymentsProviderFactoryMock.Object,
                _repaymentValidatorMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async void GetRepaymentByAccountId_ShouldReturn404_WhenAssociatedLoanOrRepaymentIsNotFound()
        {
            var accountId = new Guid();
            //Arrange
            _loanRepositoryMock.Setup(x => x.GetLoanByAccountIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<LoanModel>(null));
            _repaymentsRepositoryMock.Setup(x => x.GetRepaymentByAccountIdASync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<RepaymentsModel>(null));

            //Acct
            var response = await _sut.GetRepaymentByAccountId(accountId);


            //Assert
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

    }
}