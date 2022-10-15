using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    public class HistoryElement
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public HistoryElement(DateTime date,string description)
        {
            Date = date;
            Description = description;
        }
    }
}
