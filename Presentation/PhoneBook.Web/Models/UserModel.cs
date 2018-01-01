﻿namespace PhoneBook.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }  
        public string Department { get; set; }
        public string Manager { get; set; }
    }
}