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
    public class CustomerAccessor
    {
        /*
         * Gets a DB connection and returns a customer list from the DB
         */ 
        public static List<Customer> FetchCustomerList(Active recordType = Active.active)
        {
            // first, create the return object collection
            var customers = new List<Customer>();

            // get a connection from our connection object provider class
            var conn = DBConnection.getDBConnection();

            // we need a query (known in ADO.NET as command text)
            string query = @" SELECT CustomerID, FirstName, LastName, CreatedEmployeeID, Active " +
                           @" FROM Customers ";
            if (recordType == Active.active)
            {
                query += @" WHERE Active = 1 ";
            }
            else if (recordType == Active.inactive)
            {
                query += @" WHERE Active = 0 ";
            }
          
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
                        // we need to new up an customer
                        var currentCustomer = new Customer();
                        currentCustomer.CustomerID = reader.GetString(0);   
                        currentCustomer.FirstName = reader.GetString(1);   
                        currentCustomer.LastName = reader.GetString(2); 
                        currentCustomer.CreatedEmployeeID = reader.GetInt32(3);
                        currentCustomer.Active = reader.GetBoolean(4);     


                        // add the currentCustomer to the list
                        customers.Add(currentCustomer);
                    }
                }
                else
                {
                    // this would be a place to throw an application exception if we
                    // did not want to return an empty List<Customer>
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
            return customers;
        }

        /*
         * Gets a DB connection and returns the customer count from the DB
         */ 
        public static int FetchCustomerCount(Active recordType = Active.active)
        {
            int recordCount = 0;

            // get a connection
            var conn = DBConnection.getDBConnection();

            // create command text
            string cmdText = @" SELECT COUNT(*) " + " FROM Customers ";

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
        * Gets DB connection and inserts a customer into the DB
        */ 
        public static int InsertCustomer(Customer cust, int employeeID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.getDBConnection();
            string cmdText = @" INSERT INTO Customers " +
                string.Format(@" (CustomerID, FirstName, LastName, CreatedEmployeeID) VALUES ('{0}', '{1}', '{2}', '{3}') ",
                cust.CustomerID, cust.FirstName, cust.LastName, employeeID);

            var cmd = new SqlCommand(cmdText, conn);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

    }
}

