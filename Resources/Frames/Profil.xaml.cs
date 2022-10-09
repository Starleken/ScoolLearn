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
using System.Data.SqlClient;
using ScoolLearn.Resources.Scripts;
using ScoolLearn.Resources.WindowForProfil;

namespace ScoolLearn.Resources.Frames
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Profil : Page
    {

        static private Profil instance;

        private IConnection connection;

        private int idUser;

        public Profil(int idUser, IConnection connection)
        {
            InitializeComponent();

            this.connection = connection;

            this.idUser = idUser;

            instance = this;

            RefreshInfo();
        }

        static public Profil GetInstance()
        {
            return instance;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeLogin changeLogin = new ChangeLogin(idUser, connection);
            changeLogin.Show();
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(idUser, connection);
            changePassword.Show();
        }

        private void MailButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeMail changeMail = new ChangeMail(idUser, connection);
            changeMail.Show();
        }

        public void RefreshInfo()
        {
            throw new Exception();
        }
    }
}
