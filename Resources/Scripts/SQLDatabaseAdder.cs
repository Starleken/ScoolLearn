using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoolLearn.Resources.Scripts.Factory;
using System.Data.SqlClient;

namespace ScoolLearn.Resources.Scripts
{
    internal class SQLDatabaseAdder : IAdder
    {
        private IConnection connection;

        public SQLDatabaseAdder(IConnection connection)
        {
            this.connection = connection;
        }

        public void AddService(Service service)
        {
            string cmdCommand = $"INSERT INTO {TableNames.serviceTableName} VALUES ('{service.Title}', {service.Cost}," +
                $" {service.DurationInSeconds}, NULL ,{service.Discount}, '{service.ImagePath}')";

            try
            {
                SqlCommand command = new SqlCommand(cmdCommand, (SqlConnection)connection.GetConnection());

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new AddException("Ошибка добавления услуги");
            }
        }
    }
}
