using BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MrFlixMVC.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        CustomerManager cusgr = new CustomerManager();
        // GET: Customer
        public ActionResult Index()
        {
            var model = BuisnessLogic.CustomerManager.GetCustomerList();
            return View(model);
        }


        // GET: AddCustomer
        public ActionResult AddCustomer()
        {
            return View();
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer([Bind(Include = "CustomerID, FirstName, LastName")] string customerID, string firstName, string lastName)
        {
            int userID = RetrieveUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    cusgr.AddNewCustomer(customerID, firstName, lastName, userID);

                    ViewBag.StatusMessage = "New Customer Added!";
                    return RedirectToAction("Index", "Customer");
                }
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = ex.Message;
            }
            return View();
        }
         private int RetrieveUserId()
        {
            int userId = 0;

            var userName = User.Identity.GetUserName();

            if (null != userName)
            {
                userId = new EmployeeManager().RetrieveEmployeeId(userName);
            }

            return userId;
        }
    }
}