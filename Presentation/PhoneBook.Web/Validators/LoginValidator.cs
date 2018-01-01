using FluentValidation;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email format is wrong");
        }
    }
}