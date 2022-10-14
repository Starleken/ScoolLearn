using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class UpdateDataException : Exception
    {
        public UpdateDataException() { }

        public UpdateDataException(string message) :base(message) { }
    }
}
