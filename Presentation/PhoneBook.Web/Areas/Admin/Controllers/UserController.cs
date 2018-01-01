#region Usings

using System;
using System.Linq;
using System.Web.Mvc;
using PhoneBook.Admin.Infrastructure;
using PhoneBook.Admin.Models;
using PhoneBook.Core;
using PhoneBook.Services.Departments;
using PhoneBook.Services.Users;

#endregion

namespace PhoneBook.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctors

        public UserController(IUserService userService,
            IDepartmentService departmentService,
            IWorkContext workContext)
        {
            _userService = userService;
            _departmentService = departmentService;
            _workContext = workContext;
        }

        #endregion

        #region Utils

        [NonAction]
        protected virtual void PrepareAllUsersModel(UserModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.Users.Add(new SelectListItem
            {
                Text = "[None]",
                Value = "0"
            });
            var users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                if (user.Id == model.Id)
                    continue;

                model.Users.Add(new SelectListItem
                {
                    Text = user.FirstName + " " + user.LastName,
                    Value = user.Id.ToString()
                });
            }
        }

        [NonAction]
        protected virtual void PrepareAllDepartmentsModel(UserModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.Departments.Add(new SelectListItem
            {
                Text = "[None]",
                Value = "0"
            });
            var departments = _departmentService.GetAllDepartments();
            foreach (var department in departments)
            {
                model.Departments.Add(new SelectListItem
                {
                    Text = department.Name,
                    Value = department.Id.ToString()
                });
            }
        }

        #endregion

        #region Actions

        #region CRUD    

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new UserListModel();

            var users = _userService.GetAllUsers();
            if (users.Any())
                model.Users = users.Select(x => x.ToModel()).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new UserModel();
            PrepareAllUsersModel(model);
            PrepareAllDepartmentsModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = model.ToEntity();
                    _userService.InsertUser(user);
                    SuccessNotification("Successfully inserted");
                }
                return RedirectToAction("List");
            }
            catch
            {
                PrepareAllUsersModel(model);
                PrepareAllDepartmentsModel(model);
                ErrorNotification("An error occurred");
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return RedirectToAction("List");

            var model = user.ToModel();
            PrepareAllUsersModel(model);
            PrepareAllDepartmentsModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            try
            {
                var user = _userService.GetUserById(model.Id);
                if (user == null)
                    return RedirectToAction("List");

                if (ModelState.IsValid)
                {
                    user = model.ToEntity(user);
                    _userService.UpdateUser(user);
                    SuccessNotification("Successfully editted");
                }
                return RedirectToAction("List");
            }
            catch
            {
                PrepareAllUsersModel(model);
                PrepareAllDepartmentsModel(model);
                ErrorNotification("An error occurred");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                    return RedirectToAction("List");

                var manager = _userService.GetUserByManagerId(user.Id);
                if (manager != null)
                {
                    ErrorNotification("If a user is a manger of any user, that user can't be deleted");
                    return RedirectToAction("List");
                }

                if (user.Role == "Admin")
                {
                    ErrorNotification("Admin can't be deleted");
                    return RedirectToAction("List");
                }

                if (ModelState.IsValid)
                {
                    _userService.DeleteUser(user);
                    SuccessNotification("Successfully deleted");
                }

                return RedirectToAction("List");
            }
            catch
            {
                ErrorNotification("An error occurred");
                return RedirectToAction("Edit", new { id });
            }
        }

        #endregion

        #region Change password 

        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = _workContext.CurrentUser;
                    if (currentUser.Password != model.OldPassword)
                    {
                        ErrorNotification("OldPassword isn't corrent");
                        return View(model);
                    }

                    var user = _userService.GetUserById(currentUser.Id);
                    user.Password = model.NewPassword;
                    _userService.UpdateUser(user);

                    SuccessNotification("Successfully changed");
                    return RedirectToAction("Index", "Home");
                }
                ErrorNotification("The credentials provided is incorrect");
                return View(model);
            }
            catch
            {
                ErrorNotification("An error occurred");
                return View(model);
            }
        }

        #endregion

        #endregion
    }
}