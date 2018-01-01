using System.Web;
using System.Web.Mvc;
using PhoneBook.Core;
using PhoneBook.Core.Engine;

namespace PhoneBook.Web.Framework.Controllers
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContextBase)
        {
            var workContext = EngineContext.Current.IoCManager.Resolve<IWorkContext>();
            return workContext.CurrentUser?.Role == "Admin";
        }
    }
}
