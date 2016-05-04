using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObjects
{
    /*
     * Records object
     */ 
    public class Records
    {
        public int OrderID { get; set; }
        public string MovieTitle { get; set; }
        public string CustomerID { get; set; }
        public int CheckOutEmployeeID { get; set; }
        public DateTime DateCheckedOut { get; set; }
        public int CheckInEmployeeID { get; set; }
        public DateTime DateCheckedIn { get; set; }
        public bool Active { get; set; }

        public Records() { }    //default constructor

        public Records(int orderID, string movieTitle, string customerID, int checkOutEmployeeID,
                        DateTime dateCheckedOut, int checkInEmployee, DateTime dateCheckedIn, bool active)
        {
            OrderID = orderID;
            MovieTitle = movieTitle;
            CustomerID = customerID;
            CheckOutEmployeeID = checkOutEmployeeID;
            DateCheckedOut = dateCheckedOut;
            CheckInEmployeeID = checkInEmployee;
            DateCheckedIn = dateCheckedIn;
            Active = active;
        }
    }
}