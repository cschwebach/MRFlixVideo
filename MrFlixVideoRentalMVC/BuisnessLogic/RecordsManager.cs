using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessObjects;
using DataAccess;

namespace BuisnessLogic
{
    public class RecordsManager
    {
        /*
         * Logic method for getting a list of movies that are checked out at the moment
         */ 
        public static List<Records> GetRecordsList()
        {
            try
            {
                return RecordAccessor.FetchCheckedOutList();
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("No records found"); 
            }
        }

        /*
         * Logic method for getting all completed(checked out and checked in) records
         */ 
        public static List<Records> GetAllRecordsList()
        {
            List<Records> allRecords = null;
            try
            {
               allRecords= RecordAccessor.FetchRecordsList();
            }
            catch (ApplicationException)
            {

                throw new ApplicationException("No records found"); 
            }
            return allRecords;
        }

        /*
         * Logic method handling the parameters that are getting sent to the data access layer to be put in the database
         */ 
        public void AddNewRecord(int employeeID, string movieTitle, string customerID)
        {
            var newRecord = new Records()
            {
                MovieTitle = movieTitle,
                CustomerID = customerID
            };

            if (customerID.Length != 10)
            {
                throw new ApplicationException("Customer Phone Number is Invalid!");
            }

            if (employeeID == 0 || employeeID == null)
            {
                throw new ApplicationException("Must be logged in to Check-Out a Movie!");
            }

            try
            {
                if (RecordAccessor.CheckOutMovie(newRecord, employeeID) == 1)
                {
                    return;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid Movie Title");
            }
        }

        /*
         * Logic method for handling the parameters to alter the checkoutrecords table using the data access layer 
         */ 
        public bool CheckInMovie(int employeeId, string movieTitle)
        {
            bool result = false;
            if (employeeId == 0 || employeeId == null)
            {
                throw new ApplicationException("Must be logged in to Check-In Movie!");
            }

            if (movieTitle.Length > 75)
            {
                throw new ApplicationException("Invalid Movie Title!");
            }
            else if (movieTitle.Length < 1)
            {
                throw new ApplicationException("Invalid Movie Title");
            }

            try
            {
                var count = RecordAccessor.CheckInMovie(employeeId, movieTitle);
                result = 0 != count ? true : false;
                if (count != 0)
                {
                    result = true;
                }
                else if (count == 0)
                {
                    result = false;
                    throw new ApplicationException("Invalid Movie Title!");
                }
                else
                {
                    throw new ApplicationException("Multiple Record Found!");
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid Movie title");
            }

            return result;
        }
    }
}
