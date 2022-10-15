using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ScoolLearn.Resources.Scripts
{
    internal class SQLDatabaseUpdater : IUpdater
    {
        private IConnection connection;

        public SQLDatabaseUpdater(IConnection connection)
        {
            this.connection = connection;
        }

        public void UpdateService(Service service)
        {
            SqlConnection conn = (SqlConnection)connection.GetConnection();

            string stringCommand = $"UPDATE {service.GetTableName()} SET Title = '{service.Title}', Cost = {service.Cost}," +
                $" DurationInSeconds = {service.DurationInSeconds}, Discount = {service.Discount.ToString().Replace(',','.')}, MainImagePath = '{service.ImagePath}' WHERE ID = {service.GetId()}";

            SqlCommand command = new SqlCommand(stringCommand, conn);

            command.ExecuteNonQuery();
        }
    }
}
