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

namespace ScoolLearn
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private SqlConnection connection;

        private int idUser;

        private Service service;
        private Profil profil;
        static public History history = new History();
        static public Purchases purchases = new Purchases();

        public MainMenu(SqlConnection connection, int idUser)
        {
            InitializeComponent();

            this.connection = connection;

            this.idUser = idUser;

            SetName();

            ExitButton.Visibility = Visibility.Collapsed;

            if (Role.RoleLevel == 2 || Role.RoleLevel == 3)
            {
                addButton.Visibility = Visibility.Collapsed;
            }

            if (Role.RoleLevel != 3)
            {
                ButtonPurchases.Visibility = Visibility.Collapsed;
            }
        }

        private void SetName()
        {
            connection.Open();

            string sqlExspression = $"SELECT * FROM [User] WHERE [id] = {idUser}";

            SqlCommand cmd = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            NameTextBlock.Text = $"{reader["LastName"]} {reader["FirstName"]}";

            connection.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddService addService = new AddService(connection);

            addService.Show();
        }

        private void ButtonService_Click(object sender, RoutedEventArgs e)
        {
            if (service == null)
            {
                service = Service.GetInstance();
            }

            FrameText.Text = "Список услуг";

            FrameList.Navigate(service);

            if (Role.RoleLevel == 1)
            {
                addButton.Visibility = Visibility.Visible;
            }

            ExitButton.Visibility = Visibility.Collapsed;
        }

        private void ButtonProfil_Click(object sender, RoutedEventArgs e)
        {
            if (profil == null)
            {
                profil = new Profil(idUser);
            }

            FrameText.Text = "Профиль";

            FrameList.Navigate(profil);

            addButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Visible;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow avtorization = new MainWindow();

            avtorization.Show();

            this.Close();
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            if (history == null)
            {
                history = new History();
            }

            FrameText.Text = "История действий";

            FrameList.Navigate(history);

            addButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Collapsed;
        }

        private void ButtonPurchases_Click(object sender, RoutedEventArgs e)
        {
            if (purchases == null)
            {
                purchases = new Purchases();
            }

            FrameText.Text = "Купленные курсы";

            FrameList.Navigate(purchases);

            addButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Collapsed;
        }
    }
}
