using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    public class User
    {
        public int? Id { get; private set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public string Patronymic { get; private set; }

        public string Email { get; set; }

        public Role Role { get; private set; }

        public User() { }

        public User(string login, string password, string name, string lastName, string patronymic, string email, int roleId, int? id)
        {
            Login = login;
            Password = password;
            Name = name;
            LastName = lastName;
            Role = (Role)roleId;
            Patronymic = patronymic;
            Email = email;
            Id = id;
        }
    }
}
