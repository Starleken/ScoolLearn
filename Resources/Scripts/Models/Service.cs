using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoolLearn.Resources.Scripts
{
    internal class Service : IDeletable
    {
        public string Title { get; set; }
        public double Cost { get; set; }
        public int DurationInSeconds { get; set; }
        public double Discount { get; set; }
        public string ImagePath { get; set; }

        public Service() { }

        public Service(string title, double cost, int durationInSeconds, double discount, string imagePath)
        {
            Title = title;
            Cost = cost;
            DurationInSeconds = durationInSeconds;
            Discount = discount;
            ImagePath = imagePath;
        }
    }
}
