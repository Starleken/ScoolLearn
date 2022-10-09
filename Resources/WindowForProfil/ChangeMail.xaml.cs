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

namespace ScoolLearn.Resources.WindowForProfil
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class ChangeMail : Window
    {
        IConnection connection;

        int idUser;

        public ChangeMail(int idUser, IConnection connection)
        {
            InitializeComponent();

            this.idUser = idUser;

            this.connection = connection;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private bool CheckFilling()
        {
            if (MailTextBox.Text != "")
            {
                return true;
            }
            else return false;
        }
    }
}
