using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /*
     * Connection string class foer the DB
     */ 
    internal class DBConnection
    {
        const string ConnectionString = @"Data Source=localhost;Initial Catalog=MrFlixVideoRentals;Integrated Security=True";

        public static SqlConnection getDBConnection() // only accessible by DA classes
        {
            return new SqlConnection(ConnectionString);
        }

    }
}
