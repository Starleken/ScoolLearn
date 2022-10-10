using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    public class User
    {
        public string Login { get; private set; }

        public string Password { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public Role Role { get; private set; }

        public User()
        {

        }

        public User(string login, string password, string name, string lastName, int roleId)
        {
            Login = login;
            Password = password;
            Name = name;
            LastName = lastName;
            Role = (Role)roleId;
        }
    }
}
