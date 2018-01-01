using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using PhoneBook.Admin.Validators;

namespace PhoneBook.Admin.Models
{
    [Validator(typeof(ChangePasswordValidator))]
    public class ChangePasswordModel 
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}