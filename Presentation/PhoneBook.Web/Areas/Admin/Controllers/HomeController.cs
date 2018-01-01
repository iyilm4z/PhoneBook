using System.Web.Mvc;

namespace PhoneBook.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}