using System.Collections.Generic;

namespace PhoneBook.Web.Models
{
    public class UserListModel
    {
        public UserListModel()
        {
            Users = new List<UserModel>();
        }
        public List<UserModel> Users { get; set; }
    }
}