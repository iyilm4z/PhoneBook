using System.Collections.Generic;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Factories
{
    public interface IUserModelFactory
    {
        List<UserModel> PrepareUserModels();
        UserModel PrepareUserModelDetails(int id);
    }
}