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
                    int? id = null;
                    if (reader["ID"] != DBNull.Value)
                    {
                        id = Convert.ToInt32(reader["Id"]);
                    }

                    Service service = new ServiceFactory().Get
                        (
                        reader["Title"].ToString(),
                        Convert.ToDouble(reader["Cost"]),
                        Convert.ToInt32(reader["DurationInSeconds"]),
                        Convert.ToDouble(reader["Discount"]),
                        reader["MainImagePath"].ToString(),
                        id
                        );

                    services.Add(service);
                }

            }

            return services;
        }

        public void CheckUniquenessUser(string login)
        {
            string cmdText = $"SELECT * FROM [User] WHERE [Login]='{login}'";

            using (SqlDataReader reader = MakeQueue(cmdText))
            {
                if (reader.HasRows)
                    throw new LoginNotUniqueException("Логин уже занят");
            }
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
                        reader["Patronymic"].ToString(),
                        reader["Email"].ToString(),
                        Convert.ToInt32(reader["IdRole"]),
                        Convert.ToInt32(reader["Id"])
                        );
                }

                return user;
            }
        }

        public List<Client> GetClientsByService(Service service)
        {
            string cmdText = $"SELECT * FROM Client c INNER JOIN ClientService cs ON c.ID = cs.ClientID WHERE cs.ServiceID = {service.GetId()}";

            List<Client> clients = new List<Client>();

            using (SqlDataReader reader = MakeQueue(cmdText))
            {
                while (reader.Read())
                {
                    Client client = new ClientFactory().Get
                        (
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["Patronymic"].ToString(),
                            Convert.ToInt32(reader["ID"])
                        );

                    clients.Add(client);
                }
            }

            return clients;
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
