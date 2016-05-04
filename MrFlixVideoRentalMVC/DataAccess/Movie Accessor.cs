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
    public class MovieAccessor
    {
        /*
         * This class gets a DB connection and returns a movie list from the DB
         */ 
        public static List<Movie> FetchMovieList(Active recordType = Active.active)
        {
            // first, create the return object collection
            var movies = new List<Movie>();

            // get a connection from our connection object provider class
            var conn = DBConnection.getDBConnection();

            // we need a query (known in ADO.NET as command text)
            string query = @" SELECT MovieTitle, GenreType, CreatedEmployeeID, Director, YearReleased, Active" +
                           @" FROM Movies ";
            if (recordType == Active.active)
            {
                query += @" WHERE active = 1 ";
            }
            else if (recordType == Active.inactive)
            {
                query += @" WHERE active = 0 ";
            }
            // if Active.all, we do nothing


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
                        // we need to new up an movie
                        var currentMovie = new Movie();
                        currentMovie.MovieTitle = reader.GetString(0);
                        currentMovie.GenreType = reader.GetString(1);
                        currentMovie.CreatedEmployeeID = reader.GetInt32(2);
                        currentMovie.Director = reader.GetString(3);
                        currentMovie.YearReleased = reader.GetString(4);
                        currentMovie.Active = reader.GetBoolean(5);


                        // add the currentMovie to the list
                        movies.Add(currentMovie);
                    }
                }
                else
                {
                    // this would be a place to throw an application exception if we
                    // did not want to return an empty List<Employee>
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
            return movies;
        }

        /*
         * This class gets a DB connection and returns a movie count from the DB
         */ 
        public static int FetchMovieCount(Active recordType = Active.active)
        {
            int recordCount = 0;

            // get a connection
            var conn = DBConnection.getDBConnection();

            // create command text
            string cmdText = @" SELECT COUNT(*) " + " FROM Movies ";

            if (recordType == Active.active)
            {
                cmdText += @" WHERE active = 1 ";
            }
            else if (recordType == Active.inactive)
            {
                cmdText += @" WHERE active = 0 ";
            }
            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                recordCount = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return recordCount;
        }

        /*
         * This class gets a DB connection and inserts a movie record into the DB
         */ 
        public static int InsertMovie(Movie movie, int employeeID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.getDBConnection();
            string cmdText = @" INSERT INTO Movies " +
                string.Format(@" (MovieTitle, GenreType, CreatedEmployeeID, Director, YearReleased) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}' ) ",
                movie.MovieTitle, movie.GenreType, employeeID, movie.Director, movie.YearReleased);

            var cmd = new SqlCommand(cmdText, conn);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

    }
}
