using System.Collections.Generic;
using PhoneBook.Core.Domain;

namespace PhoneBook.Services.Departments
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentById(int departmentId);
        void InsertDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);
    }
}