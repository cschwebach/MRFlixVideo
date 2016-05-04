using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessObjects;
using DataAccess;

namespace BuisnessLogic
{
    public class SecurityManager
    {
        const int MIN_USERNAME = 3;
        const int MIN_PASSWORD = 5;

        /*
         * Logic method for validating the user 
         */
        public static AccessToken ValidateExistingUser(string username, string password)
        {
            AccessToken accessToken;

            if (username.Length < MIN_USERNAME || password.Length < MIN_PASSWORD)
            {
                throw new ApplicationException("Invalid username or password. Username has to be least 3 characters, password has to be at least 5 charcters!");
            }

            try
            {

                if (1 == EmployeeAccessor.FindEmployeeByUsernameAndPassword(username, password.HashSha256()))
                {
                    var emp = EmployeeAccessor.RetrieveEmployeeByUsername(username);
                    accessToken = new AccessToken(emp);
                }
                else {
                    throw new ApplicationException("Invalid User Name And/Or password.");
                }
               
            }
            catch
            {
                throw new ApplicationException("Invalid User Name And/Or password.");
            }
            return accessToken;
        }

        /*
         * Logic method for validating a new user
         */ 
        public static AccessToken ValidateNewUser(string username, string newPassword)
        {
            // check for new user
            if (1 == EmployeeAccessor.FindEmployeeByUsernameAndPassword(username, "Movies"))
            {
                EmployeeAccessor.SetPasswordForUsername(username, "Movies", newPassword.HashSha256());
            }
            else {
                throw new ApplicationException("Invalid data.");
            }
            return ValidateExistingUser(username, newPassword);
        }
    }
}
