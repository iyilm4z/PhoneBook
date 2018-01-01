using FluentValidation;
using PhoneBook.Admin.Models;

namespace PhoneBook.Admin.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("OldPassword is required");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("NewPassword is required");
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage("ConfirmNewPassword is required");
            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("Passwords don't match");
        }
    }
}