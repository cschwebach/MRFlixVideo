using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BuisnessObjects
{
    /*
     * Reviews Object
     */ 
    public class Reviews
    {
        public int ReviewID { get; set; }

        [MinLength(1), MaxLength(75)]
        [Required(ErrorMessage = "Please enter a Movie Title!")]
        public string MovieTitle { get; set; }

        [MinLength(4), MaxLength(100)]
        [Required(ErrorMessage = "Please enter a Movie Review between 4 and 75 characters in length!")]
        public string MovieReview { get; set; }

        public Reviews() { }   // default constructor

        public Reviews(
                        int reviewID,
                        string movieTitle, 
                        string movieReview)
        {
            ReviewID = reviewID;
            MovieTitle = movieTitle;
            MovieReview = movieReview;
        }
    }
}

