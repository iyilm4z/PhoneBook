#region Usings

using System;
using System.Linq;
using System.Web.Mvc;
using PhoneBook.Core;
using PhoneBook.Services.Authentication;
using PhoneBook.Services.Departments;
using PhoneBook.Services.Users;
using PhoneBook.Web.Factories;
using PhoneBook.Web.Models;

#endregion

namespace PhoneBook.Web.Controllers
{
    public class UserController : BasePublicController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctors

        public UserController(IUserService userService,
            IDepartmentService departmentService,
            IUserModelFactory userModelFactory,
            IAuthenticationService authenticationService,
            IWorkContext workContext)
        {
            _userService = userService;
            _departmentService = departmentService;
            _userModelFactory = userModelFactory;
            _authenticationService = authenticationService;
            _workContext = workContext;
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new UserListModel
            {
                Users = _userModelFactory.PrepareUserModels()
            };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = _userModelFactory.PrepareUserModelDetails(id);
            return View(model);
        }

        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ValidateUser(model.Email, model.Password))
                {
                    var customer = _userService.GetUserByEmail(model.Email);
                    _authenticationService.SignIn(customer, model.RememberMe);

                    return RedirectToAction("Index", "Home");
                }
                ErrorNotification("The credentials provided is incorrect");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}