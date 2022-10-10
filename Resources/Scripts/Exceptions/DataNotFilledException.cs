using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class DataNotFilledException : Exception
    {
        public DataNotFilledException() { }

        public DataNotFilledException(string message) : base(message) { }
    }
}
