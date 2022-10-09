using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ScoolLearn.Resources.Scripts
{
    public interface IConnection
    {
        void TryOpenConnection();

        DbConnection GetConnection();
    }
}
