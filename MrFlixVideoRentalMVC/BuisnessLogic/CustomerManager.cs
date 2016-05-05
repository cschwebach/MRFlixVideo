using BuisnessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BuisnessLogic
{
    public class CustomerManager
    {
        /*
         * Logic method that communicates with the data access layer to get the list of cutomers
         */
        public static List<Customer> GetCustomerList()
        {
            try
            {
                return CustomerAccessor.FetchCustomerList();
            }
            catch (Exception)
            {

                throw new ApplicationException("No records found");
            }
        }

        /*
         * Logic method that gets the customer count from the data access layer
         */ 
        public int GetCustomerCount(Active type)
        {
            int count = 0;
            try
            {
                count = CustomerAccessor.FetchCustomerCount(type);
            }
            catch (Exception)
            {
                throw new ApplicationException("No movie count returned.");
            }
            return count;
        }

       /*
        * Logic method that handles the parameters to add a new customer through the data access layer
        */ 
        public void AddNewCustomer(string customerID, string firstName, string lastName, int employeeID)
        {
            var newCust = new Customer()
            {
                CustomerID = customerID,
                FirstName = firstName,
                LastName = lastName,

            };

            if (firstName.Length < 2)
            {
                throw new ApplicationException("First name must be must be at least 2 characters long!");
            }

            if (customerID.Length != 10)
            {
                throw new ApplicationException("Customers local phone number must be 10 digits");
            }

            if (lastName.Length < 2)
            {
                throw new ApplicationException("Last name must be at least 2 characters long!");
            }


            try
            {
                if (CustomerAccessor.InsertCustomer(newCust, employeeID) == 1)
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


