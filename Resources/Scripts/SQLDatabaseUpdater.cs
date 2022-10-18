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
            string stringCommand = $"UPDATE {service.GetTableName()} SET Title = '{service.Title}', Cost = {service.Cost}," +
                $" DurationInSeconds = {service.DurationInSeconds}, Discount = {service.Discount.ToString().Replace(',','.')}, MainImagePath = '{service.ImagePath.Replace(@"\Resources\Images\","")}' WHERE ID = {service.GetId()}";

            MakeQueue(stringCommand);
        }

        public void UpdateUser(User user)
        {
            string stringCommand = $"UPDATE [{TableNames.userTableName}] SET Login = '{user.Login}', Password = '{user.Password}'," +
                $" FirstName = '{user.Name}', LastName = '{user.LastName}', Patronymic = '{user.Patronymic}', Email = '{user.Email}' WHERE Id = {user.Id}";

            MakeQueue(stringCommand);
        }

        private void MakeQueue(string cmdText)
        {
            try
            {
                SqlConnection conn = (SqlConnection)connection.GetConnection();

                SqlCommand command = new SqlCommand(cmdText, conn);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new UpdateException("Не удалось обновить информацию");
            }
        }
    }
}
