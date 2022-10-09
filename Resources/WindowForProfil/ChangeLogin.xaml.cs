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
        SqlConnection connection;

        int idUser;

        public ChangeLogin(int idUser)
        {
            InitializeComponent();

            this.idUser = idUser;

            connection = Connection.GetConnection();
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

            connection.Open();

            string sqlExspression = $"UPDATE [User] SET [Login]='{LoginTextBox.Text}' WHERE id={idUser}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            command.ExecuteNonQuery();

            connection.Close();

            Profil.GetInstance().RefreshInfo();

            MainMenu.history.AddHistory("Изменён логин");

            this.Close();
        }

        private bool CheckIdentifity()
        {
            connection.Open();

            string sqlExpression = $"SELECT * FROM [User] WHERE [Login]='{LoginTextBox.Text}'";

            SqlCommand command = new SqlCommand(sqlExpression, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                MessageBox.Show("Логин занят");

                connection.Close();

                return false;
            }
            else
            {
                connection.Close();

                return true;
            }
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
