using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BuisnessObjects
{
    /*
     * Movies Object
     */ 
    public class Movie
    {
        [MinLength(1), MaxLength(75)]
        [Required(ErrorMessage = "Please enter a Movie Title between 1 and 75 characters in length!")]
        public string MovieTitle { get; set; }

        [MinLength(4), MaxLength(20)]
        [Required(ErrorMessage = "Please enter a valid movie genre!")]
        public string GenreType { get; set; }
        public int CreatedEmployeeID { get; set; }

        [MinLength(2), MaxLength(75)]
        [Required(ErrorMessage = "Please enter a Director between 2 and 75 charcters in length!")]
		public string Director { get; set; }

        [MinLength(4), MaxLength(4)]
        [Required(ErrorMessage = "Please enter a 4 digit year!")]
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
