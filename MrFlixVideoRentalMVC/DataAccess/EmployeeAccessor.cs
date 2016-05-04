using BuisnessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeAccessor
    {
       
        /*
         * This class gets a DB connection and returns a employees list from the DB
         */ 
        public static List<Employee> FetchEmployeeList(Active recordType = Active.active)
        {
            // first, create the return object collection
            var employees = new List<Employee>();

            // get a connection from our connection object provider class
            var conn = DBConnection.getDBConnection();

            // we need a query (known in ADO.NET as command text)
            string query = @" SELECT EmployeeID, UserName, FirstName, LastName, Email, Active " +
                           @" FROM Employees ";
            if (recordType == Active.active)
            {
                query += @" WHERE Active = 1 ";
            }
            else if (recordType == Active.inactive)
            {
                query += @" WHERE Active = 0 ";
            }
            // if Active.all, we do nothing


            // create a sqlcommand object
            var cmd = new SqlCommand(query, conn);

            // at this point, we need to worry about connection exceptions

            try
            {
                // open the connection
                conn.Open();

                // execute the command to return a SqlDataReader
                var reader = cmd.ExecuteReader();

                // trying to process the reader will throw exceptions if
                // it is empty, so we should check for rows
                if (reader.HasRows)
                {
                    // we can process the reader
                    while (reader.Read())
                    {
                        // we need to new up an employee
                        var currentEmployee = new Employee();
                        currentEmployee.EmployeeID = reader.GetInt32(0);   
                        currentEmployee.UserName = reader.GetString(1);
                        currentEmployee.FirstName = reader.GetString(2);    
                        currentEmployee.LastName = reader.GetString(3);     
                        currentEmployee.Email = reader.GetString(4);
                        currentEmployee.Active = reader.GetBoolean(5);     


                        // add the currentEmployee to the list
                        employees.Add(currentEmployee);
                    }
                }
                else
                {
                    // this would be a place to throw an application exception if we
                    // did not want to return an empty List<Employee>
                    throw new ApplicationException("Your query returned no data.");
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            // if the query does not succeed, this will be empty...
            return employees;
        }

        /*
         * This class gets a DB connection and returns a employees count from the DB
         */ 
        public static int FetchEmployeeCount(Active recordType = Active.active)
        {
            int recordCount = 0;

            // get a connection
            var conn = DBConnection.getDBConnection();

            // create command text
            string cmdText = @" SELECT COUNT(*) " + " FROM Employees ";

            if (recordType == Active.active)
            {
                cmdText += @" WHERE active = 1 ";
            }
            else if (recordType == Active.inactive)
            {
                cmdText += @" WHERE active = 0 ";
            }
            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                recordCount = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return recordCount;
        }

        /*
         * This class gets a DB connection and updates the employee phone number
         */ 
        public static int UpdateEmployeeEmail(int employeeID, String newEmail)
        {
            int rowCount = 0;

            // get a connection
            var conn = DBConnection.getDBConnection();

            // we need a command object (the command text is in the stored procedure)
            var cmd = new SqlCommand("sp_Update_Employee_Email", conn);

            // set the command type for stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to manually add any input or output parameteres
            cmd.Parameters.Add(new SqlParameter("Email", SqlDbType.VarChar, 50));
            cmd.Parameters.Add(new SqlParameter("EmployeeID", SqlDbType.Int));

            cmd.Parameters["Email"].Value = newEmail;
            cmd.Parameters["EmployeeID"].Value = employeeID;

            cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
            cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        /*
         * This class gets a DB connection and inserts a new employee record into the DB
         */ 
        public static int InsertEmployee(Employee emp)
        {
            int result = 0 ;

            // What comes first...a connection! Eureka!
            var conn = DBConnection.getDBConnection();

            var query = @"spInsertEmployees";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", emp.UserName);
            cmd.Parameters.AddWithValue("@Password", emp.Password);
            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@Email", emp.Email);

           
            try
            {
                // open the connection
                conn.Open();

                result = cmd.ExecuteNonQuery(); 
               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static Employee RetrieveEmployeeByUsername(string username)
        // accepts a username, returns the corresponding employee object or a null reference
        {
            Employee employee;
            var conn = DBConnection.getDBConnection();
            var query = @"spSelectEmployeeByUserName";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    employee = new Employee()
                    {
                        EmployeeID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        Email = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };
                }
                else
                {
                    throw new ApplicationException("Data not found");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return employee;
        }

        /*
         * This class gets a DB connection and finds the username and password for validation
         */ 
        public static int FindEmployeeByUsernameAndPassword(string username, string password)
        {
            int count = 0;
            var conn = DBConnection.getDBConnection();
            var query = @"sp_Validate_Active_Username_And_Password";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        /*
         * This class gets a DB connection and sets a password according to the username
         */ 
        public static int SetPasswordForUsername(string username, string oldPassword, string newPassword)
        {
            int count = 0;
            var conn = DBConnection.getDBConnection();
            var query = @"sp_Update_Password_For_Username";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
            cmd.Parameters.AddWithValue("@newPassword", newPassword);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        
    }
}
