using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScoolLearn.Resources.Frames
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class History : Page
    {
        public class Write
        {
            public string Date { get; set; }
            public string Text { get; set; }
        }
        public History()
        {
            InitializeComponent();
        }

        public void AddHistory(string text)
        {
            Write write = new Write()
            {
                Date = DateTime.Now.ToString(),
                Text = text
            };

            HistoryDataGrid.Items.Add(write);
        }
    }
}
