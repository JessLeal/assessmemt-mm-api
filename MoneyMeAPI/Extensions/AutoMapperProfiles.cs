using AutoMapper;
using MoneyMeAPI.Constants;
using MoneyMeAPI.DTOs;
using MoneyMeAPI.Model;
using System.Diagnostics.CodeAnalysis;

namespace MoneyMeAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateLoanDto, LoanModel>()
                .ForPath(dest => dest.Account.Title, opts => opts
                    .MapFrom(src => src.Title))
                .ForPath(dest => dest.Account.FirstName, opts => opts
                    .MapFrom(src => src.FirstName))
                .ForPath(dest => dest.Account.LastName, opts => opts
                    .MapFrom(src => src.LastName))
                .ForPath(dest => dest.Account.DateOfBirth, opts => opts
                    .MapFrom(src => src.DateOfBirth))
                .ForPath(dest => dest.Account.Mobile, opts => opts
                    .MapFrom(src => src.Mobile))
                .ForPath(dest => dest.Account.Email, opts => opts
                    .MapFrom(src => src.Email));

            CreateMap<LoanModel, CreateLoanDto>()
                .ForMember(dest => dest.Title, opts => opts
                    .MapFrom(src => src.Account.Title))
                .ForMember(dest => dest.FirstName, opts => opts
                    .MapFrom(src => src.Account.FirstName))
                .ForMember(dest => dest.LastName, opts => opts
                    .MapFrom(src => src.Account.LastName))
                .ForMember(dest => dest.DateOfBirth, opts => opts
                    .MapFrom(src => src.Account.DateOfBirth))
                .ForMember(dest => dest.Mobile, opts => opts
                    .MapFrom(src => src.Account.Mobile))
                .ForMember(dest => dest.Email, opts => opts
                    .MapFrom(src => src.Account.Email));

            CreateMap<LoanModel, LoanDto>()
                .ForMember(dest => dest.Product, opts => opts
                    .MapFrom(src => src.Product != null ? src.Product : DefaultValues.DEFAULT_PRODUCT))
                .ForMember(dest => dest.Title, opts => opts
                    .MapFrom(src => src.Account.Title))
                .ForMember(dest => dest.FirstName, opts => opts
                    .MapFrom(src => src.Account.FirstName))
                .ForMember(dest => dest.LastName, opts => opts
                    .MapFrom(src => src.Account.LastName))
                .ForMember(dest => dest.DateOfBirth, opts => opts
                    .MapFrom(src => src.Account.DateOfBirth))
                .ForMember(dest => dest.Mobile, opts => opts
                    .MapFrom(src => src.Account.Mobile))
                .ForMember(dest => dest.Email, opts => opts
                    .MapFrom(src => src.Account.Email));

            CreateMap<LoanDto, LoanModel>()
                .ForPath(dest => dest.Account.Title, opts => opts
                    .MapFrom(src => src.Title))
                .ForPath(dest => dest.Account.FirstName, opts => opts
                    .MapFrom(src => src.FirstName))
                .ForPath(dest => dest.Account.LastName, opts => opts
                    .MapFrom(src => src.LastName))
                .ForPath(dest => dest.Account.DateOfBirth, opts => opts
                    .MapFrom(src => src.DateOfBirth))
                .ForPath(dest => dest.Account.Mobile, opts => opts
                    .MapFrom(src => src.Mobile))
                .ForPath(dest => dest.Account.Email, opts => opts
                    .MapFrom(src => src.Email));

            CreateMap<RepaymentsDto, RepaymentsModel>()
                .ForPath(dest => dest.Account.Title, opts => opts
                    .MapFrom(src => src.Loan.Title))
                .ForPath(dest => dest.Account.FirstName, opts => opts
                    .MapFrom(src => src.Loan.FirstName))
                .ForPath(dest => dest.Account.LastName, opts => opts
                    .MapFrom(src => src.Loan.LastName))
                .ForPath(dest => dest.Account.DateOfBirth, opts => opts
                    .MapFrom(src => src.Loan.DateOfBirth))
                .ForPath(dest => dest.Account.Mobile, opts => opts
                    .MapFrom(src => src.Loan.Mobile))
                .ForPath(dest => dest.Account.Email, opts => opts
                    .MapFrom(src => src.Loan.Email));

            CreateMap<AccountModel, AccountDto>();
        }
    }
}
