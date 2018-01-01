using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.Core.Data;
using PhoneBook.Core.Domain;

namespace PhoneBook.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.Table.ToList();
        }

        public User GetUserById(int userId)
        {
            if (userId == 0)
                return null;

            return _userRepository.GetById(userId);
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return _userRepository.Table.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByManagerId(int managerId)
        {
            if (managerId == 0)
                return null;

            return _userRepository.Table.SingleOrDefault(u => u.ManagerId == managerId);
        }

        public bool ValidateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            if (string.IsNullOrEmpty(password))
                return false;

            var user = GetUserByEmail(email);

            if (user == null)
                return false;

            var isValid = user.Password == password;
            return isValid;
        }

        public void InsertUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userRepository.Update(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userRepository.Delete(user);
        }
    }
}