using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObjects
{
    /*
     * Reviews Object
     */ 
    public class Reviews
    {
        public int ReviewID { get; set; }
        public string MovieTitle { get; set; }
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

