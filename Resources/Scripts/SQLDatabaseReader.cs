using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ScoolLearn.Resources.Scripts.Factory;
using System.Data.Common;

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

        public List<Service> ReadServices()
        {
            List<Service> services = new List<Service>();

            using (SqlDataReader reader = MakeQueue("SELECT * FROM Service"))
            {
                while (reader.Read())
                {
                    Service service = new ServiceFactory().Get
                        (
                        reader["Title"].ToString(),
                        Convert.ToDouble(reader["Cost"]),
                        Convert.ToInt32(reader["DurationInSeconds"]),
                        Convert.ToDouble(reader["Discount"]),
                        reader["MainImagePath"].ToString()
                        );

                    services.Add(service);
                }

            }

            return services;
        }

        public User FindUser(string login, string password)
        {
            User user = null;

            using (SqlDataReader reader = MakeQueue($"SELECT * FROM [User] WHERE [Login]='{login}' AND [Password]='{password}'"))
            {
                while (reader.Read())
                {
                    user = new UserFactory().Get
                        (
                        reader["Login"].ToString(),
                        reader["Password"].ToString(),
                        reader["FirstName"].ToString(),
                        reader["LastName"].ToString(),
                        Convert.ToInt32(reader["IdRole"])
                        );
                }

                return user;
            }
        }

        private SqlDataReader MakeQueue(string cmdText)
        {
            sqlConn = (SqlConnection)connection.GetConnection();

            SqlCommand cmd = new SqlCommand(cmdText, sqlConn);

            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }
    }
}
