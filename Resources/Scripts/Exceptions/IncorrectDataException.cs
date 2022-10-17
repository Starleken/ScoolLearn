using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class IncorrectDataException : Exception
    {
        public IncorrectDataException() { }

        public IncorrectDataException(string message) : base(message) { }
    }
}
