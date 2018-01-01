using System.Collections.Generic;
using PhoneBook.Core.Data;

namespace PhoneBook.Core.Domain
{
    public class Department : BaseEntity
    {
        private ICollection<User> _users;

        public string Name { get; set; }

        public virtual ICollection<User> Users
        {
            get => _users ?? (_users = new List<User>());
            protected set => _users = value;
        }
    }
}