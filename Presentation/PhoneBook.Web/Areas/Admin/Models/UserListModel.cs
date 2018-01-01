using System.Collections.Generic;

namespace PhoneBook.Admin.Models
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