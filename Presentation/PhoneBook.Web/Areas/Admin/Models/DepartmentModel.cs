using FluentValidation.Attributes;
using PhoneBook.Admin.Validators;

namespace PhoneBook.Admin.Models
{
    [Validator(typeof(DepartmentValidator))]
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}