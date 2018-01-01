#region Usings

using System.Linq;
using System.Web.Mvc;
using PhoneBook.Admin.Infrastructure;
using PhoneBook.Admin.Models;
using PhoneBook.Services.Departments;

#endregion

namespace PhoneBook.Admin.Controllers
{
    public class DepartmentController : BaseAdminController
    {
        #region Fields

        private readonly IDepartmentService _departmentService;

        #endregion

        #region Ctors

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new DepartmentListModel();

            var departments = _departmentService.GetAllDepartments();
            if (departments.Any())
                model.Departments = departments.Select(x => x.ToModel()).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new DepartmentModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var department = model.ToEntity();
                    _departmentService.InsertDepartment(department);
                    SuccessNotification("Successfully inserted");
                }
                return RedirectToAction("List");
            }
            catch
            {
                ErrorNotification("An error occurred");
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var department = _departmentService.GetDepartmentById(id);
            if (department == null)
                return RedirectToAction("List");

            var model = department.ToModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentModel model)
        {
            try
            {
                var department = _departmentService.GetDepartmentById(model.Id);
                if (department == null)
                    return RedirectToAction("List");

                if (ModelState.IsValid)
                {
                    department = model.ToEntity(department);
                    _departmentService.UpdateDepartment(department);
                    SuccessNotification("Successfully editted");
                }
                return RedirectToAction("List");
            }
            catch
            {
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
                var department = _departmentService.GetDepartmentById(id);
                if (department == null)
                    return RedirectToAction("List");

                if (department.Users.Any())
                {
                    ErrorNotification("If a department has any user, that department can't be deleted");
                    return RedirectToAction("Edit", new { id });
                }

                if (ModelState.IsValid)
                {
                    _departmentService.DeleteDepartment(department);
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
    }
}