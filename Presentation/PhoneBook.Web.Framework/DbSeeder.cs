using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.Core.Domain;
using PhoneBook.Core.Engine;
using PhoneBook.Services.Departments;
using PhoneBook.Services.Users;

namespace PhoneBook.Web.Framework
{
    public class DbSeeder
    {
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;

        public DbSeeder()
        {
            _userService = EngineContext.Current.IoCManager.Resolve<IUserService>();
            _departmentService = EngineContext.Current.IoCManager.Resolve<IDepartmentService>();
        }

        private void InsertDepartments()
        {
            var departments = new List<Department>
            {
                new Department
                {
                    Name = "Sales"
                },
                new Department
                {
                    Name = "Marketing"
                },
                new Department
                {
                    Name = "Accounting"
                },
                new Department
                {
                    Name = "Production"
                },
                new Department
                {
                    Name = "Human Resources"
                }
            };

            foreach (var department in departments)
                _departmentService.InsertDepartment(department);
        }

        private void InsertUsers()
        {
            var departments = _departmentService.GetAllDepartments();

            var users = new List<User>
            {
                new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "jhon@doe.com",
                    Password = "123",
                    Role = "Admin",
                    PhoneNumber = "963-986-5078-14",
                    Address = "123 6th St. Melbourne, FL 32904",
                    DepartmentId = departments.First().Id
                },
                new User
                {
                    FirstName = "Maya",
                    LastName = "Buttreeks",
                    PhoneNumber = "610-986-5078-14",
                    Address = "71 Pilgrim Avenue Chevy Chase, MD 20815",
                    ManagerId = 1,
                    DepartmentId = departments.First().Id
                },
                new User
                {
                    FirstName = "Ivana",
                    LastName = "Tinkle",
                    PhoneNumber = "181-679-1265-98",
                    Address = "44 Shirley Ave. West Chicago, IL 60185",
                    DepartmentId = departments.First().Id
                },
                new User
                {
                    FirstName = "Mike",
                    LastName = "Litorus",
                    PhoneNumber = "91-530-7103-11",
                    Address = "4 Goldfield Rd. Honolulu, HI 96815",
                    ManagerId = 2,
                    DepartmentId = departments.Last().Id
                },
                new User
                {
                    FirstName = "Seth",
                    LastName = "Poole",
                    PhoneNumber = "78-890-9760-34",
                    Address = "70 Bowman St. South Windsor, CT 06074",
                    DepartmentId = departments.Last().Id
                },
            };

            foreach (var user in users)
                _userService.InsertUser(user);
        }

        private void DeleteDepartments()
        {
            var departments = _departmentService.GetAllDepartments();

            foreach (var department in departments)
                _departmentService.DeleteDepartment(department);
        }

        private void DeleteUsers()
        {
            var users = _userService.GetAllUsers();

            foreach (var user in users)
                _userService.DeleteUser(user);
        }

        public void Seed()
        {
            if (!_departmentService.GetAllDepartments().Any())
            {
                DeleteDepartments();
                InsertDepartments();
                DeleteUsers();
                InsertUsers();
            }
        }
    }
}