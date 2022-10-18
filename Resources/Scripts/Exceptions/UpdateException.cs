using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class UpdateException : Exception
    {
        public UpdateException() { }

        public UpdateException(string message) :base(message) { }
    }
}
