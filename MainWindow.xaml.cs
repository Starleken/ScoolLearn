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
        string password = "";
        bool passwordIsHide = true;

        private SqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();

            PasswordTextBox.Visibility = Visibility.Hidden;

            Connection.SetConnection();

            connection = Connection.GetConnection();
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
            connection.Open();

            string sqlExpression = $"SELECT * FROM [User] WHERE [Login]='{LoginTextBox.Text}' AND [Password]='{password}'";

            SqlCommand command = new SqlCommand(sqlExpression, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                connection.Close();

                return true;
            }
            else
            {
                MessageBox.Show("Проверьте логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                connection.Close();

                return false;
            }
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();

            if (connection == null)
            {
                MessageBox.Show("Не удалось установить соединение с базой данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

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

            if (!CheckFilling())
            {
                MessageBox.Show("Заполните данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                connection.Close();

                return;
            }

            connection.Close();

            if (!CheckAccuracy())
            {
                return;
            }

            int id = FindIdUserByLogin();

            Role.UserId = id;

            Role.RoleLevel = FindRoleUserById(id);

            MainMenu window = new MainMenu(connection, id);
            window.Show();

            this.Close();
        }

        private int FindIdUserByLogin()
        {
            connection.Open();

            string sqlExspression = $"SELECT ID FROM [User] WHERE [Login] = '{LoginTextBox.Text}'";

            SqlCommand cmd = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            int id = (int)reader["id"];

            connection.Close();

            return id;
        }

        private int FindRoleUserById(int id)
        {
            connection.Open();

            string sqlExspression = $"SELECT IdRole FROM [User] WHERE [Id] = {id}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            int role = Convert.ToInt32(reader["IdRole"]);

            connection.Close();

            return role;
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
            Registration registration = new Registration();

            registration.Show();

            this.Close();
        }
    }
}
