using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    public interface IHistoryHandler
    {
        void AddHistory(string text);
    }
}
