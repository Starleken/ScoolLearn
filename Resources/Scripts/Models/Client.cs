using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    public class Client
    {
        public int? Id { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public string Patronymic { get; private set; }

        public Client() { }

        public Client(int? id, string name, string lastName, string patronymic)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Patronymic = patronymic;
        }
    }
}
