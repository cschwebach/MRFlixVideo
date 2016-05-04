using BuisnessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BuisnessLogic
{
    public class ReviewManager
    {
        /*
         * Logic method that handles the review list from the Data access layer
         */
        public static List<Reviews> GetReviewList()
        {
            try
            {
                return ReviewAccessor.FetchReviewList();
            }
            catch (Exception)
            {
                
                throw new ApplicationException("Returned no data"); 
            }
        }

        /*
         * Logic method that adds a new review giving parameters to the data access layer
         */ 
        public void AddNewReview(string movieTitle, string movieReview)
        {
            if (movieReview.Length < 4) 
            {
                throw new ApplicationException("Please enter at least a 4 character review!");
            }
            var newCustReview = new Reviews()
            {
               
                MovieTitle = movieTitle,
                MovieReview = movieReview,
            };

            try
            {
                if (ReviewAccessor.InsertReview(newCustReview) == 1)
                {
                    return;
                }
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("Invalid Movie Title");
            }
        }
    }
}

