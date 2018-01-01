using System;
using PhoneBook.Core.Domain;

namespace PhoneBook.Services.Users
{
    public static class UserExtensions
    {
        //public static bool IsInCustomerRole(this User user,
        //    string customerRoleSystemName, bool onlyActiveCustomerRoles = true)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException("user");

        //    if (String.IsNullOrEmpty(customerRoleSystemName))
        //        throw new ArgumentNullException("userRoleSystemName");

        //    var result = user.CustomerRoles
        //                     .Where(cr => !onlyActiveCustomerRoles || cr.Active)
        //                     .Where(cr => cr.SystemName == customerRoleSystemName)
        //                     .FirstOrDefault() != null;
        //    return result;
        //}
        //public static bool IsAdmin(this User user, bool onlyActiveCustomerRoles = true)
        //{
        //    return IsInCustomerRole(customer, SystemCustomerRoleNames.Administrators, onlyActiveCustomerRoles);
        //}
    }
}
