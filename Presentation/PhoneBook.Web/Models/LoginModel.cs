using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using PhoneBook.Web.Validators;

namespace PhoneBook.Web.Models
{
    [Validator(typeof(LoginValidator))]
    public class LoginModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}