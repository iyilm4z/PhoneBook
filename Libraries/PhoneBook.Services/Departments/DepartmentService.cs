using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.Core.Data;
using PhoneBook.Core.Domain;

namespace PhoneBook.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;

        public DepartmentService(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public List<Department> GetAllDepartments()
        {
            return _departmentRepository.Table.ToList();
        }

        public Department GetDepartmentById(int departmentId)
        {
            if (departmentId == 0)
                return null;

            return _departmentRepository.GetById(departmentId);
        }

        public void InsertDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            _departmentRepository.Insert(department);
        }

        public void UpdateDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            _departmentRepository.Update(department);
        }

        public void DeleteDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            _departmentRepository.Delete(department);
        }
    }
}
