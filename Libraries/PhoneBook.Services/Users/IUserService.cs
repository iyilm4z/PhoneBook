using System.Collections.Generic;
using PhoneBook.Core.Domain;

namespace PhoneBook.Services.Users
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        User GetUserByManagerId(int managerId);
        bool ValidateUser(string email, string password);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);    
    }
}