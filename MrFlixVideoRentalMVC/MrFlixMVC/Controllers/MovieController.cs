using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace MrFlixMVC.Controllers
{
    public class MovieController : Controller
    {
        private MovieManager mvmgr = new MovieManager();
        private RecordsManager rcmgr = new RecordsManager();

        // GET: Movie
        public ActionResult ViewMovies()
        {
            var model = BuisnessLogic.MovieManager.GetMovieList();

            return View(model);
        }

        [Authorize]
        //Get: Add Movie
        public ActionResult AddMovie()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie([Bind(Include = "MovieTitle, GenreType, Director, YearReleased")] string movieTitle, string genreType, string director, string yearReleased)
        {
            int employeeID = RetrieveUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    mvmgr.AddNewMovie(movieTitle, employeeID, genreType, director, yearReleased);
                   
                    ViewBag.StatusMessage = "Movie Added!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = ex.Message;
            }
            return View();
        }

        [Authorize]
        //Get: Check Out Movie
        public ActionResult CheckOut()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut([Bind(Include = "MovieTitle, CustomerID")] string movieTitle, string CustomerID)
        {
            int employeeID = RetrieveUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    rcmgr.AddNewRecord(employeeID, movieTitle, CustomerID);

                    ViewBag.StatusMessage = "Movie Checked Out Successfully!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = ex.Message;
            }
            return View();
        }

        [Authorize]
        //Get: Check In Movie
        public ActionResult CheckIn()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "MovieTitle")] string movieTitle)
        {
            int employeeID = RetrieveUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    rcmgr.CheckInMovie(employeeID, movieTitle);

                    ViewBag.StatusMessage = "Movie Checked In Successfully!";
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