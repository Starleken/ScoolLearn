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
        private string password = "";
        private bool passwordIsHide = true;

        private IConnection connection;

        public MainWindow()
        {
            InitializeComponent();

            PasswordTextBox.Visibility = Visibility.Hidden;

            connection = new SQLDatabaseConnection(AppConnection.sqlStringPath);

            TryOpenConnection();

        }

        private void TryOpenConnection()
        {
            try
            {
                connection.TryOpenConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckFilling()
        {
            if (password != "" && LoginTextBox.Text != "")
            {
                return;
            }
            else throw new DataNotFilledException("Введите данные");
        }

        private User FindUser()
        {
            SQLDatabaseReader reader = new SQLDatabaseReader(connection);
            User user =  reader.FindUser(LoginTextBox.Text, password);

            if (user == null)
            {
                throw new UserNotFoundException("Неверный логин или пароль"); 
            }

            return user;
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

            try
            {
                CheckFilling();
                User user = FindUser();

                OpenWindow(new MainMenu(connection, user));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            OpenWindow(new Registration(connection));
        }

        private void OpenWindow(Window window)
        {
            window.Show();
            this.Close();
        }
    }
}
