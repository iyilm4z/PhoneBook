#region Usings

using System;
using System.Web;
using System.Web.Security;
using PhoneBook.Core.Domain;
using PhoneBook.Services.Users;

#endregion

namespace PhoneBook.Services.Authentication
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly IUserService _customerService;
        private readonly TimeSpan _expirationTimeSpan;

        private User _cachedUser;

        #endregion

        #region Ctors

        public FormsAuthenticationService(HttpContextBase httpContext,
            IUserService userService)
        {
            _httpContext = httpContext;
            _customerService = userService;
            _expirationTimeSpan = FormsAuthentication.Timeout;
        }

        #endregion

        #region Utils

        private User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            var email = ticket.UserData;

            if (string.IsNullOrWhiteSpace(email))
                return null;
            var user = _customerService.GetUserByEmail(email);
            return user;
        }

        #endregion

        #region Methods

        public void SignIn(User user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Email,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                user.Email,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedUser = user;
        }

        public void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public User GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext?.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null)
                _cachedUser = user;
            return _cachedUser;
        }

        #endregion
    }
}