using BuisnessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BuisnessLogic
{
    public class EmployeeManager
    {
        public int RetrieveEmployeeId(string userName)
        {
            int userId = 0;

            try
            {
                userId = EmployeeAccessor.RetrieveEmployeeByUsername(userName).EmployeeID;
            }
            catch (Exception) { } // userId will be set to zero.

            return userId;
        }

        public Employee GetEmployeeByUserName(string username)
        {
            try
            {
                return EmployeeAccessor.RetrieveEmployeeByUsername(username);
            }
            catch (Exception)
            {
                throw;
            }

        }
        /*
         * Logic method for getting a list of employees from the data access layer
         */ 
        public static List<Employee> GetEmployeeList()
        {
            try
            {
                return EmployeeAccessor.FetchEmployeeList();
            }
            catch (Exception)
            {
               
                throw new ApplicationException("No records found"); 
            }
        }

        /*
         * Logic method for gettting the number of employees from the data access layer
         */ 
        public int GetEmployeeCount(Active type)
        {
            int count = 0;
            try
            {
                count = EmployeeAccessor.FetchEmployeeCount(type);
            }
            catch (Exception)
            {
                throw new ApplicationException("No employee count returned.");
            }
            return count;
        }

        /*
         * Logic method for changing the employees ID in the data access layer
         */
        public bool ChangeEmployeeEmail(int employeeID, string email)
        {
            bool result = false;
            if (email.Length < 5)
            {
                throw new ApplicationException("Invalid email");
            }
            else if (employeeID < 100000)
            {
                throw new ApplicationException("Invalid Employee ID");
            }

            try
            {
                var count = EmployeeAccessor.UpdateEmployeeEmail(employeeID, email);
                result = 0 != count ? true : false;
                if (count != 0)
                {
                    result = true;
                }
                else if (count == 0)
                {
                    result = false;
                    throw new ApplicationException("Invalid employeeID!");
                }
                else
                {
                    throw new ApplicationException("Multiple Records Found!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /**
         * Logic method for adding a new employee to the database
        */
        public int AddNewEmployee(string username,  string password, string firstName, string lastName, string email)
        {

            int result = 0;
            var newEmp = new Employee()
            {
                UserName=username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            if (username.Length < 3 )
            {
                throw new ApplicationException("Username must be at least 3 characters!");
            }

            if (firstName.Length < 2)
            {
                throw new ApplicationException("First name must be at least 2 characters!");
            }

            if (lastName.Length < 3)
            {
                throw new ApplicationException("Last name must be at least 3 characters!");
            }

            if (email.Length < 5) 
            {
                throw new ApplicationException("Invalid email!");
            }

            try
            {
                if (EmployeeAccessor.InsertEmployee(newEmp) == 1)
                {
                    //result = 1;
                    return result ;
                }
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("Invalid EmployeeID");
            }
            return result;
        }

       
       
    }
}

