using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ScoolLearn.Resources.Scripts
{
    internal class SQLDatabaseDeleter : IDeleter
    {
        private IConnection connection;

        public SQLDatabaseDeleter(IConnection connection)
        {
            this.connection = connection;
        }

        public void DeleteObject(IDeletable obj)
        {
            string stringCommand = "DELETE FROM {} WHERE ";

            try
            {
                SqlCommand command = new SqlCommand(stringCommand);
            }
            catch (Exception)
            {
                throw new DeleteException("Ошибка удаления");
            }
        }
    }
}
