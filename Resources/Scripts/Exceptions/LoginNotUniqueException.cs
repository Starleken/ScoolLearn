using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class LoginNotUniqueException : Exception
    {
        public LoginNotUniqueException() { }

        public LoginNotUniqueException(string message) : base(message) { }
    }
}
