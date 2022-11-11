

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
    public class LoanControllerTest
    {
        private readonly LoanController _sut;
        private readonly Mock<ILoanRepository> _loanRepositoryMock = new Mock<ILoanRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IURLGeneratorService> _urlGeneratorServiceMock = new Mock<IURLGeneratorService>();

        public LoanControllerTest()
        {
            _sut = new LoanController(_loanRepositoryMock.Object, _mapperMock.Object, _urlGeneratorServiceMock.Object);
        }

        [Fact]
        public async void GetLoanByIdAccount_ShouldReturn404_WhenAssociatedLoanIsNotFound()
        {
            var accountId = new Guid();
            //Arrange
            _loanRepositoryMock.Setup(x => x.GetLoanByAccountIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<LoanModel>(null));

            //Acct
            ActionResult<LoanDto> response = await _sut.GetLoanByAccountId(accountId);


            //Assert
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async void GetLoanByIdAccount_ShouldReturn200Ok_WhenAssociatedLoanIsFound()
        {
            //Arrange
            var accountId = new Guid();
            var loanModel = new LoanModel
            {
                Amount = 100,
                Term = 2,
                Account = new AccountModel
                {
                    Title = "Mr",
                    FirstName = "Jess",
                }
            };

            var loanDto = new LoanDto
            {
                Amount = loanModel.Amount,
                Term = loanModel.Term,
            };


            _loanRepositoryMock.Setup(x => x.GetLoanByAccountIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(loanModel);
            _mapperMock.Setup(x => x.Map<LoanDto>(It.IsAny<LoanModel>())).Returns(loanDto);

            //Acct
            ActionResult<LoanDto> response = await _sut.GetLoanByAccountId(accountId);


            //Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async void GetLoanByIdAccount_ShouldThrow_WhenErrorOccurs()
        {
            //Arrange
            var accountId = new Guid();

            _loanRepositoryMock.Setup(x => x.GetLoanByAccountIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());

            //Acct
            var act = async () => await _sut.GetLoanByAccountId(accountId);

            //Assert
            await Assert.ThrowsAsync<Exception>(act);
        }
    }
}