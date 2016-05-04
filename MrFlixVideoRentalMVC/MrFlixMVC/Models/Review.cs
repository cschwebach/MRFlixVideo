using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MrFlixMVC.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        [Required]
        [Display(Name = "MovieTitle")]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "MovieReview")]
        public string Code { get; set; }

    }
}