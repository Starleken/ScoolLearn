using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal interface IReader
    {
        void CheckUniquenessUser(string login);

        User FindUser(string login, string password);

        List<Service> ReadServices();

        List<Client> GetClientsByService(Service service);
    }
}
