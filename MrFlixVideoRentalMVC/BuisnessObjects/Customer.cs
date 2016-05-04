using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObjects
{
    /*
     * Customer Object
     */ 
    public class Customer
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
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

