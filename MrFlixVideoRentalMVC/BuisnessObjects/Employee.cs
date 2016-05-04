using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObjects
{
    /*
     * Employee Object
     */ 
    public class Employee
    {
        // if this won't need to be serialized, consider
        // making the EmployeeID property setter private
        public int EmployeeID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public Employee() { }   // default constructor

        public Employee(int employeeID,
                        string userName,
                        string password,
                        string firstName,
                        string lastName,
                        string email,
                        bool active)
        {
            EmployeeID = employeeID;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Active = active;
        }
    }
}

