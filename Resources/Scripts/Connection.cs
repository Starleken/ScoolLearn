using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace ScoolLearn.Resources.Scripts
{
    static internal class Connection
    {
        static public string connectionString = @"Server=DESKTOP-QM5BSJ8\SQLEXPRESS;Database=lang2;Trusted_Connection=True;";

        static public SqlConnection connection { get; private set; }

        static public void ChangeConnection(string url, string databaseName)
        {
            connectionString = $"Server={url};Database={databaseName};Trusted_Connection=True;";
        }

        static public void SetConnection()
        {
            connection = new SqlConnection(connectionString);
        }

        static public SqlConnection GetConnection()
        {
            if (connection != null)
            {
                return connection;
            }
            else return null;
        }
    }
}
