using PhoneBook.Core;
using PhoneBook.Core.Domain;
using PhoneBook.Services.Authentication;

namespace PhoneBook.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        private readonly IAuthenticationService _authenticationService;

        public WebWorkContext(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public User CurrentUser => _authenticationService.GetAuthenticatedUser();

        public bool IsAdmin => !string.IsNullOrEmpty(CurrentUser?.Role) && CurrentUser.Role == "Admin";
    }
}