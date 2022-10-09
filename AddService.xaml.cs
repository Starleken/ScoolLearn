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
using System.Windows.Shapes;
using System.Data.SqlClient;
using ScoolLearn.Resources.Scripts;
using ScoolLearn.Resources.Frames;

namespace ScoolLearn
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddService : Window
    {
        private IConnection connection;

        public AddService(IConnection connection)
        {
            InitializeComponent();

            this.connection = connection;
        }

        private  void addButton_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private bool CheckFilling()
        {
            if (nameTextBox.Text != "" && priceTextBox.Text != "" && timeTextBox.Text != null)
            {
                return true;
            }
            return false;
        }
    }
}
