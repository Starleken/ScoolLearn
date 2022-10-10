using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts.Factory
{
    internal class UserFactory
    {
        public User Get(string login, string password)
        {
            User user = new User(login, password);

            return user;
        }
    }
}
