using FluentValidation;
using PhoneBook.Admin.Models;

namespace PhoneBook.Admin.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required");
        }
    }
}