using PhoneBook.Core.Domain;

namespace PhoneBook.Services.Authentication
{
    public interface IAuthenticationService
    {
        void SignIn(User user, bool createPersistentCookie);
        void SignOut();
        User GetAuthenticatedUser();
    }
}