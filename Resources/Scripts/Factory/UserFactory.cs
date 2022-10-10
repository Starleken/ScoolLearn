using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts.Factory
{
    internal class UserFactory
    {
        public User Get(string login, string password, string name, string lastName, int roleId)
        {
            User user = new User(login, password, name, lastName, roleId);

            return user;
        }
    }
}
