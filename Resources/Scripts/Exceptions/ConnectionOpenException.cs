﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class ConnectionOpenException : Exception
    {
        public ConnectionOpenException() { }

        public ConnectionOpenException(string message) : base(message) { }
    }
}
