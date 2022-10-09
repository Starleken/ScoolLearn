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
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class ChangeService : Window
    {
        SqlConnection connection;

        int idService = 0;

        public ChangeService(int id)
        {
            InitializeComponent();

            idService = id;

            FillTextBox();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void FillTextBox()
        {
            throw new Exception();
        }
    }
}
