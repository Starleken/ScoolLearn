using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data.Common;

namespace ScoolLearn.Resources.Scripts
{
    internal class SQLDatabaseConnection : IConnection
    {
        private SqlConnection connection;

        public SQLDatabaseConnection(string connectionPath)
        {
            connection = new SqlConnection(connectionPath);
        }

        public DbConnection GetConnection()
        {
            return connection;
        }

        public void TryOpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
