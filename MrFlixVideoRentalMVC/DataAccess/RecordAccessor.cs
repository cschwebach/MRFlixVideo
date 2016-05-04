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
    public class RecordAccessor
    {
        /*
         * This class gets a DB connection and returns a checkout list from the DB
         */ 
        public static List<Records> FetchCheckedOutList(Active recordType = Active.active)
        {
            // first, create the return object collection
            var records = new List<Records>();

            // get a connection from our connection object provider class
            var conn = DBConnection.getDBConnection();

            // we need a query (known in ADO.NET as command text)
            string query = @" SELECT OrderID, CheckOutEmployeeID , MovieTitle, CustomerID, DateCheckedOut " +
                           @" FROM CheckOutRecords ";
            if (recordType == Active.active)
            {
                query += @" WHERE active = 1 AND DateCheckedIn IS NULL AND CheckInEmployeeID IS NULL ";
            }
            else if (recordType == Active.inactive)
            {
                query += @" WHERE active = 0 AND DateCheckedIn IS NULL AND CheckInEmployeeID IS NULL";
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
                        // we need to new up an record
                        var currentRecord = new Records();
                        currentRecord.OrderID = reader.GetInt32(0);
                        currentRecord.CheckOutEmployeeID = reader.GetInt32(1);
                        currentRecord.MovieTitle = reader.GetString(2);
                        currentRecord.CustomerID = reader.GetString(3);
                        currentRecord.DateCheckedOut = reader.GetDateTime(4);
                      
                        records.Add(currentRecord);
                    }
                }
                else
                {
                    
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
            return records;
        }

        /*
         * This class gets a DB connection and returns a records list(checked out and checked In) from the DB
         */ 
        public static List<Records> FetchRecordsList(Active recordType = Active.active)
        {
            // first, create the return object collection
            var records = new List<Records>();

            // get a connection from our connection object provider class
            var conn = DBConnection.getDBConnection();

            // we need a query (known in ADO.NET as command text)
            string query = @" SELECT OrderID, CheckOutEmployeeID , MovieTitle, CustomerID, DateCheckedOut, CheckInEmployeeID, DateCheckedIn, Active " +
                           @" FROM CheckOutRecords ";
            if (recordType == Active.active)
            {
                query += @" WHERE active = 1 AND  CheckInEmployeeID IS NOT NULL  AND DateCheckedIn IS NOT NULL ";
            }
            else if (recordType == Active.inactive)
            {
                query += @" WHERE active = 0 AND  CheckInEmployeeID IS NOT NULL AND DateCheckedIn IS NOT NULL";
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
                        // we need to new up an movie
                        var currentRecord = new Records();
                        currentRecord.OrderID = reader.GetInt32(0);
                        currentRecord.CheckOutEmployeeID = reader.GetInt32(1);
                        currentRecord.MovieTitle = reader.GetString(2);
                        currentRecord.CustomerID = reader.GetString(3);
                        currentRecord.DateCheckedOut = reader.GetDateTime(4);
                        currentRecord.CheckInEmployeeID = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                        currentRecord.DateCheckedIn = !reader.IsDBNull(6) ? reader.GetDateTime(6) : DateTime.MinValue;
                        currentRecord.Active = reader.GetBoolean(7);

                      
                       
                        records.Add(currentRecord);
                    }
                }
                else
                {
                    
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
            return records;
        }

        /*
         * This class gets a DB connection and inserts records to checkout a movie
         */ 
        public static int CheckOutMovie(Records records, int employeeID)
        {
            int rowsAffected = 0;

            DateTime dateCheckedOut = DateTime.Now;

            var conn = DBConnection.getDBConnection();
            string cmdText = @" INSERT INTO CheckOutRecords " +
                string.Format(@" (CheckOutEmployeeID, MovieTitle, CustomerID, DateCheckedOut, CheckInEmployeeID, DateCheckedIn) VALUES ('{0}', '{1}', '{2}', '{3}', null, null) ",
                employeeID, records.MovieTitle, records.CustomerID, dateCheckedOut, null, null);

            var cmd = new SqlCommand(cmdText, conn);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("Invalid phone number or movie title"); ;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /*
         * This class gets a DB connection and uses a stored procedure to alter the checkout records table to check in a movie
         */ 
        public static int CheckInMovie(int employeeID, string movieTitle)
        {
            int rowCount = 0;

            DateTime dateCheckedIn = DateTime.Now;

            // get a connection
            var conn = DBConnection.getDBConnection();

            // we need a command object (the command text is in the stored procedure)
            var cmd = new SqlCommand("sp_CheckInMovie", conn);

            // set the command type for stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to manually add any input or output parameteres
            cmd.Parameters.Add(new SqlParameter("@MovieTitle", SqlDbType.VarChar, 75));
            cmd.Parameters.Add(new SqlParameter("@CheckInEmployeeID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@DateCheckedIn", SqlDbType.DateTime));
           

            cmd.Parameters["@MovieTitle"].Value = movieTitle;
            cmd.Parameters["@CheckInEmployeeID"].Value = employeeID;
            cmd.Parameters["@DateCheckedIn"].Value = dateCheckedIn;

            cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
            cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid Movie Title");
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        }
    }

