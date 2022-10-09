using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    static internal class Role
    {
        static public int UserId { get; set; }

        static public int RoleLevel { get; set; }

        static public int AdminCode { get; private set; } = 1111;
    }
}
