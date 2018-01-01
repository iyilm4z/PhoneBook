using System.Collections.Generic;
using System.Linq;
using PhoneBook.Services.Users;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Factories
{
    public class UserModelFactory : IUserModelFactory
    {
        private readonly IUserService _userService;

        public UserModelFactory(IUserService userService)
        {
            _userService = userService;
        }

        public List<UserModel> PrepareUserModels()
        {
            var model = new List<UserModel>();

            var users = _userService.GetAllUsers();
            if (users.Any())
            {
                foreach (var user in users)
                {
                    model.Add(new UserModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber
                    });
                }
            }

            return model;
        }

        public UserModel PrepareUserModelDetails(int id)
        {
            var model = new UserModel();

            var user = _userService.GetUserById(id);
            if (user != null)
            {
                var manager = _userService.GetUserById(user.ManagerId);
                model = new UserModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Department = user.Department?.Name,
                    Manager = manager?.FirstName + " " + manager?.LastName
                };

            }

            return model;
        }
    }
}