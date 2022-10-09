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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ChangeLogin : Window
    {
        IConnection connection;

        int idUser;

        public ChangeLogin(int idUser, IConnection connection)
        {
            InitializeComponent();

            this.idUser = idUser;

            this.connection = connection;
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFilling())
            {
                MessageBox.Show("Заполните данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (!CheckIdentifity())
            {
                return;
            }

            throw new Exception();
        }

        private bool CheckIdentifity()
        {
            throw new Exception();
        }

        private bool CheckFilling()
        {
            if (LoginTextBox.Text != "")
            {
                return true;
            }
            else return false;
        }
    }
}
