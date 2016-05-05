using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLogic;

namespace MrFlixMVC.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        EmployeeManager empgr = new EmployeeManager();

        // GET: Employee
        public ActionResult Index()
        {
            var model = BuisnessLogic.EmployeeManager.GetEmployeeList();
            return View(model);
        }

        // GET: Employee
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee([Bind(Include = "UserName, Password, FirstName, LastName, Email")] string userName, string password, string firstName, string lastName, string email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    empgr.AddNewEmployee(userName, password, firstName, lastName, email);

                    ViewBag.StatusMessage = "New Employee Added!";
                    return RedirectToAction("Index", "Employee");
                }
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = ex.Message;
            }
            return View();
        }
    }
}