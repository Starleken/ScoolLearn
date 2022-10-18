using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    public class User : Client
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; private set; }

        public User() { }

        public User(string login, string password, string name, string lastName, string patronymic, string email, int roleId, int? id) : base(id,name,lastName,patronymic)
        {
            Login = login;
            Password = password;
            Role = (Role)roleId;
            Email = email;
        }
    }
}
