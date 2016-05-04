using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObjects
{
    /*
     * Movies Object
     */ 
    public class Movie
    {  
        public string MovieTitle { get; set; }
        public string GenreType { get; set; }
        public int CreatedEmployeeID { get; set; }
		public string Director { get; set; }
		public string YearReleased { get; set; }
        public bool Active { get; set; }

        public Movie() { }    // default constructor

        public Movie(String movieTitle,
                        string genreType,
                        int createdEmployeeID,
                        string director,
						string yearReleased,
                        bool active)
        {
            MovieTitle = movieTitle;
            GenreType = genreType;
            CreatedEmployeeID = createdEmployeeID;
            Director = director;
            YearReleased = yearReleased;
            Active = active;
        }
    }
}
