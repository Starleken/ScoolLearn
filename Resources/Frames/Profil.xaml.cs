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

        private SqlConnection connection;

        private int idUser;

        public Profil(int idUser)
        {
            InitializeComponent();

            connection = Connection.GetConnection();

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
            ChangeLogin changeLogin = new ChangeLogin(idUser);
            changeLogin.Show();
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(idUser);
            changePassword.Show();
        }

        private void MailButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeMail changeMail = new ChangeMail(idUser);
            changeMail.Show();
        }

        public void RefreshInfo()
        {
            connection.Open();

            string sqlExspression = $"SELECT * FROM [User] WHERE [id] = {idUser}";

            SqlCommand command = new SqlCommand(sqlExspression,connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            FullNameTextBlock.Text = $"{reader["LastName"]} {reader["FirstName"]} {reader["MiddleName"]}";

            LastNameTextBlock.Text = reader["LastName"].ToString();

            FirstNameTextBlock.Text = reader["FirstName"].ToString();

            MiddleNameTextBlock.Text = reader["MiddleName"].ToString();

            LoginNameTextBlock.Text = reader["Login"].ToString();

            int lengthPassword = reader["Password"].ToString().Length;

            PasswordTextBlock.Text = new string('*', lengthPassword);

            MailTextBlock.Text = reader["Email"].ToString();

            TextBorder.Text = FirstNameTextBlock.Text[0].ToString();

            connection.Close();
        }
    }
}
