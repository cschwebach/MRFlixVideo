using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BuisnessObjects
{
    /*
     * Customer Object
     */ 
    public class Customer
    {
        [MinLength(10), MaxLength(10)]
        [Required(ErrorMessage = "Please enter a the Customers phone number!")]
        public string CustomerID { get; set; }

        [MinLength(1), MaxLength(50)]
        [Required(ErrorMessage = "Please enter a first name between 1 and 50 characters in length!")]
        public string FirstName { get; set; }

        [MinLength(1), MaxLength(75)]
        [Required(ErrorMessage = "Please enter a first name between 1 and 75 characters in length!")]
        public string LastName { get; set; }
        public int CreatedEmployeeID { get; set; }
        public bool Active { get; set; }

        public Customer() { }     // default constructor

        public Customer(string customerID,
                        string firstName,
                        string lastName,
                        int createdEmployeeID,
                        bool active)
        {
            CustomerID = customerID;
            FirstName = firstName;
            LastName = lastName;
            CreatedEmployeeID = createdEmployeeID;
            Active = active;
        }
    }
}

