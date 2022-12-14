using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts.Factory
{
    internal class ServiceFactory
    {
        public Service Get(string title, double cost, int duration, double discount, string imagePath, int? id)
        {
            Service service = new Service(title, cost, duration, discount, "\\Resources\\Images\\" + imagePath.Trim(), id);

            return service;
        }
    }
}
