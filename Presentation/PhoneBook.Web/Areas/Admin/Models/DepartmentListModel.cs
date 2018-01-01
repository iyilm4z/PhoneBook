using System.Collections.Generic;

namespace PhoneBook.Admin.Models
{
    public class DepartmentListModel
    {
        public DepartmentListModel()
        {
            Departments = new List<DepartmentModel>();
        }
        public List<DepartmentModel> Departments { get; set; }
    }
}