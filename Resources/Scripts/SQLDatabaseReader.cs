using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ScoolLearn.Resources.Scripts
{
    internal class SQLDatabaseReader : IReader
    {
        private IConnection connection;

        private SqlConnection sqlConn;

        public SQLDatabaseReader(IConnection connection)
        {
            this.connection = connection;
        }

        public Service[] ReadServices()
        {
            string cmdText = "";

            SqlConnection sqlconn = (SqlConnection)connection.GetConnection();

            SqlCommand cmd = new SqlCommand(cmdText, sqlconn);

            cmd.ExecuteReader();

            throw new Exception();
        }

        public User[] ReadUsers()
        {
            throw new NotImplementedException();
        }
    }
}
