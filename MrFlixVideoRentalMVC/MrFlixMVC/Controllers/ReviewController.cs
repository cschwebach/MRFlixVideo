using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLogic;

namespace MrFlixMVC.Controllers
{
    public class ReviewController : Controller
    {
        private ReviewManager rvmgr = new ReviewManager();

        // GET: Review
        public ActionResult AddReview()
        {
            return View();
        }

        // GET: Review
        public ActionResult ViewReviews()
        {
            var model = BuisnessLogic.ReviewManager.GetReviewList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview([Bind(Include = "MovieTitle, MovieReview")] string movieTitle, string movieReview)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    rvmgr.AddNewReview(movieTitle, movieReview);

                    ViewBag.StatusMessage = "Review Added!";
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