using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts.Factory
{
    internal class ClientFactory
    {
        public Client Get(string name, string lastName, string patronymic, int? id)
        {
            Client human = new Client(id, name, lastName, patronymic);

            return human;
        }
    }
}
