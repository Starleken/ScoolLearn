using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal interface IReader
    {
        User[] ReadUsers();

        Service[] ReadServices();
    }
}
