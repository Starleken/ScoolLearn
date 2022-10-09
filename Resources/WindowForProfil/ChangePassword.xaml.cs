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
using ScoolLearn.Resources.Frames;
using ScoolLearn.Resources.Scripts;


namespace ScoolLearn.Resources.WindowForProfil
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        SqlConnection connection;

        int idUser;

        public ChangePassword(int idUser)
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

            connection.Open();

            string sqlExspression = $"UPDATE [User] SET [Password]='{PasswordTextBox.Text}' WHERE id={idUser}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            command.ExecuteNonQuery();

            connection.Close();

            Profil.GetInstance().RefreshInfo();

            MainMenu.history.AddHistory("Изменён пароль");

            this.Close();
        }

        private bool CheckFilling()
        {
            if (PasswordTextBox.Text != "")
            {
                return true;
            }
            else return false;
        }
    }
}
