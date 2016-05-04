using BuisnessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BuisnessLogic
{
    public class MovieManager
    {
        /*
         * Logic method for getting a list of movies from the data access layer
         */ 
        public static List<Movie> GetMovieList()
        {
            try
            {
                return MovieAccessor.FetchMovieList();
            }
            catch (Exception)
            {

                throw new ApplicationException("No records found");
            }
        }

        /*
         * Logic method for getting the total number of movies
         */ 
        public int GetMovieCount(Active type)
        {
            int count = 0;
            try
            {
                count = MovieAccessor.FetchMovieCount(type);
            }
            catch (Exception)
            {
                throw new ApplicationException("No movie count returned.");
            }
            return count;
        }
        
        /*
         * Logic method that handles the parameters to insert a new movie into the database using the data access layer
         */
        public void AddNewMovie(string movieTitle, int employeeID, string genreType, string director, string yearReleased)
        {
            var newMovie = new Movie()
            {
                MovieTitle = movieTitle,
                GenreType = genreType,
                Director = director,
                YearReleased = yearReleased
            };

            if (movieTitle.Length < 1)
            {
                throw new ApplicationException("Must enter sum kind of text for a movie title!");
            }

            if (genreType.Length < 3)
            {
                throw new ApplicationException("Must be valid genre type!");
            }

            if (director.Length < 4)
            {
                throw new ApplicationException("Directors name must be at least 4 characters long!");
            }

            if (yearReleased.Length != 4)
            {
                throw new ApplicationException("Year released must be at least 4 digits!");
            }
            if (employeeID == 0 || employeeID == null)
            {
                throw new ApplicationException("Must be logged in to add a movie!");
            }

            try
            {
                if (MovieAccessor.InsertMovie(newMovie, employeeID) == 1)
                {
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


