using BuisnessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ReviewAccessor
    {
        /*
         * Gets a DB connection and returns a review list from the DB
         */ 
        public static List<Reviews> FetchReviewList()
        {
            // first, create the return object collection
            var reviews = new List<Reviews>();

            // get a connection from our connection object provider class
            var conn = DBConnection.getDBConnection();

            // we need a query (known in ADO.NET as command text)
            string query = @" SELECT ReviewID, MovieTitle, MovieReview" +
                           @" FROM ReviewRecords ";

            // create a sqlcommand object
            var cmd = new SqlCommand(query, conn);

            // at this point, we need to worry about connection exceptions

            try
            {
                // open the connection
                conn.Open();

                // execute the command to return a SqlDataReader
                var reader = cmd.ExecuteReader();

                // trying to process the reader will throw exceptions if
                // it is empty, so we should check for rows
                if (reader.HasRows)
                {
                    // we can process the reader
                    while (reader.Read())
                    { 
                        // we need to new up an reviews
                        var currentReview = new Reviews();
                        currentReview.ReviewID = reader.GetInt32(0);
                        currentReview.MovieTitle = reader.GetString(1);    
                        currentReview.MovieReview = reader.GetString(2);     

                        // add the currentReview to the list
                        reviews.Add(currentReview);
                    }
                }
                else
                {
                    // this would be a place to throw an application exception if we
                    // did not want to return an empty List<Reviews>
                    throw new ApplicationException("Your query returned no data.");
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            // if the query does not succeed, this will be empty...
            return reviews;
        }

        /*
         * Gets a DB connection and inserts a review into the DB
         */ 
        public static int InsertReview(Reviews review)
        {
            int rowsAffected = 0;

            var conn = DBConnection.getDBConnection();
            string cmdText = @" INSERT INTO ReviewRecords " +
                string.Format(@" (MovieTitle, MovieReview) VALUES ('{0}', '{1}') ",
                review.MovieTitle, review.MovieReview);

            var cmd = new SqlCommand(cmdText, conn);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid Movie title");
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

    }
}
