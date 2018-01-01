using FluentValidation;
using PhoneBook.Admin.Models;

namespace PhoneBook.Admin.Validators
{
    public class DepartmentValidator : AbstractValidator<DepartmentModel>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}