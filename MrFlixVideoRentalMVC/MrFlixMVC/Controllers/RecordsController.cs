using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLogic;

namespace MrFlixMVC.Controllers
{
    [Authorize]
    public class RecordsController : Controller
    {
        // GET: Records
        public ActionResult Index()
        {
            return View();
        }

        // GET: Records of all movies rented and brought back
        public ActionResult AllRecords()
        {
            var model = BuisnessLogic.RecordsManager.GetAllRecordsList();

            return View(model);
        }

        // GET: Records movies checked out
        public ActionResult MoviesOut()
        {
            try
            {
                var model = BuisnessLogic.RecordsManager.GetRecordsList();
                return View(model);
            }
            catch (Exception ex)
            {
               
                ViewBag.StatusMessage = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}