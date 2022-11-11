using FluentValidation;
using MoneyMeAPI.Constants;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace MoneyMeAPI.DTOs
{
    [ExcludeFromCodeCoverage]
    public class LoanDto
    {
        public double Amount { get; set; }
        public int Term { get; set; }
        public string Product { get; set; } = DefaultValues.DEFAULT_PRODUCT;
        public string Title { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsSaveRepayment { get; set; } = false;
    }

    public class LoanDtoValidator : AbstractValidator<LoanDto>
    {
        public LoanDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(DefaultValues.AMOUNT_MIN)
                .WithMessage($"Amount should not be less than {DefaultValues.AMOUNT_MIN}")
                .LessThanOrEqualTo(DefaultValues.AMOUNT_MAX)
                .WithMessage($"Amount should not be greater than {DefaultValues.AMOUNT_MAX}");
            RuleFor(x => x.Term)
               .GreaterThanOrEqualTo(DefaultValues.TERM_MIN)
               .WithMessage($"Term should not be less than {DefaultValues.TERM_MIN}")
               .LessThanOrEqualTo(DefaultValues.TERM_MAX)
               .WithMessage($"Term should not be less than {DefaultValues.TERM_MAX}");
            RuleFor(x => x.Product)
                .IsEnumName(typeof(ProductTypeEnum.ProductType))
                .WithMessage($"Product is not present in the product list");
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title should not be empty");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name should not be empty");
            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Last name should not be empty");
            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                .WithMessage("User must be at least 18 yrs old");
            RuleFor(x => x.Mobile)
               .NotEmpty()
               .WithMessage("Mobile number should not be empty")
               .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$"))
               .WithMessage("Mobile number should be an 11 digit number");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email should not be empty")
                .EmailAddress()
                .WithMessage("Email address is not valid");
        }
    }
}
