using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessObjects
{
    public sealed class AccessToken : Employee
    {
        /*
         * Access class, for user access (Access Object)
         */ 
        public AccessToken(Employee emp)
        {
            if (emp == null || !emp.Active)
            {
                throw new ApplicationException("Invalid Employee");
            }
            base.EmployeeID = emp.EmployeeID;
            base.UserName = emp.UserName;
            base.FirstName = emp.FirstName;
            base.LastName = emp.LastName;
            base.Email = emp.Email;
            base.Active = emp.Active;
        }
    }

}