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

namespace ScoolLearn
{
    /// <summary>
    /// Логика взаимодействия для меню регистрации.xaml
    /// </summary>
    public partial class Registration : Window
    {
        IConnection connection;

        private bool passwordIsHide = true;

        private string password;

        public Registration(IConnection connection)
        {
            InitializeComponent();

            this.connection = connection;

            PasswordTextBox.Visibility = Visibility.Hidden;
        }

        private bool CheckFilling()
        {
            if (LoginTextBox.Text != "" & NameTextBox.Text != "" && FamilyTextBox.Text != "" && MiddleNameTextBox.Text != "" && TelephoneTextBox.Text != "" 
                && EmailTextBox.Text != "" && BirthdayTextBox.Text != "" && (PasswordTextBox.Text != "" || HidePasswordBox.Password != ""))
            {
                return true;
            }
            else return false;
        }

        private bool CheckIdentifity()
        {
            throw new Exception();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFilling())
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (!CheckIdentifity())
            {
                return;
            }

            if (passwordIsHide)
            {
                password = HidePasswordBox.Password;
            }
            else
            {
                password = PasswordTextBox.Text;
            }

            this.Close();
        }

        private void HidePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordIsHide)
            {
                PasswordTextBox.Text = HidePasswordBox.Password;
                PasswordTextBox.Visibility = Visibility.Visible;
                HidePasswordBox.Visibility = Visibility.Hidden;
            }
            else
            {
                HidePasswordBox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Hidden;
                HidePasswordBox.Visibility = Visibility.Visible;
            }

            passwordIsHide = !passwordIsHide;
        }
    }
}
