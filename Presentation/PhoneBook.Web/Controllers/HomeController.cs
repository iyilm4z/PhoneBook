using System.Web.Mvc;

namespace PhoneBook.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }   
    }
}