using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using PhoneBook.Admin.Validators;

namespace PhoneBook.Admin.Models
{
    [Validator(typeof(UserValidator))]
    public class UserModel
    {
        public UserModel()
        {
            Departments = new List<SelectListItem>();
            Users = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }  

        public int DepartmentId { get; set; }
        public IList<SelectListItem> Departments { get; set; }

        public int ManagerId { get; set; }
        public IList<SelectListItem> Users { get; set; }
    }
}