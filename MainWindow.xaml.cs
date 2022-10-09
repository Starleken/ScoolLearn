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

namespace ScoolLearn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=DESKTOP-30IU0QJ\\SQLEXPRESS;Database=lang2;Trusted_Connection=True;";

        private string password = "";
        private bool passwordIsHide = true;

        private IConnection connection;

        public MainWindow()
        {
            InitializeComponent();

            PasswordTextBox.Visibility = Visibility.Hidden;

            connection = new SQLDatabaseConnection(connectionString);

            TryOpenConnection();
        }

        private void TryOpenConnection()
        {
            try
            {
                connection.TryOpenConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось установить подключение");
            }
        }

        private bool CheckFilling()
        {
            if (password != "" && LoginTextBox.Text != "")
            {
                return true;
            }
            else return false;
        }

        private bool CheckAccuracy()
        {
            SQLDatabaseReader reader = new SQLDatabaseReader(connection);

            User[] users = reader.ReadUsers();

            throw new Exception();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordIsHide)
            {
                password = HidePasswordBox.Password;
            }
            else
            {
                password = PasswordTextBox.Text;
            }

            if (!CheckFilling())
            {
                MessageBox.Show("Заполните данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (!CheckAccuracy())
            {
                return;
            }
        }

        private void HidePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if(passwordIsHide)
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

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration(connection);

            registration.Show();

            this.Close();
        }
    }
}
