using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class DeleteException : Exception
    {
        public DeleteException() { }

        public DeleteException(string message) : base(message) { }
    }
}
