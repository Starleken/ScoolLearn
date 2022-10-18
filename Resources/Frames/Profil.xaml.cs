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

        private IConnection connection;

        private User user;

        public Profil(User user, IConnection connection)
        {
            InitializeComponent();

            this.connection = connection;

            this.user = user;

            RefreshInfo();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeLogin changeLogin = new ChangeLogin(user, connection);
            changeLogin.Show();
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(user, connection);
            changePassword.Show();
        }

        private void MailButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeMail changeMail = new ChangeMail(user, connection);
            changeMail.Show();
        }

        public void RefreshInfo()
        {
            FullNameTextBlock.Text = $"{user.Name} {user.LastName} {user.Patronymic}";
            FirstNameTextBlock.Text = $"{user.Name}";
            LastNameTextBlock.Text = $"{user.LastName}";
            LoginNameTextBlock.Text = $"{user.Login}";
            PasswordTextBlock.Text = new string('*', user.Password.Length);
            MiddleNameTextBlock.Text = $"{user.Patronymic}";
            MailTextBlock.Text = $"{user.Email}";
        }
    }
}
