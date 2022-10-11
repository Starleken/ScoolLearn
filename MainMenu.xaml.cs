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
        private IConnection connection;

        private User user;

        public MainMenu(IConnection connection, User user)
        {
            InitializeComponent();

            this.connection = connection;

            this.user = user;

            NameTextBlock.DataContext = user;

            InitButton();
        }

        private void InitButton()
        {
            if (user.Role != Role.Admin)
            {
                addButton.Visibility = Visibility.Collapsed;
                return;
            }

            ButtonPurchases.Visibility = Visibility.Collapsed;
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddService addService = new AddService(connection);

            addService.Show();
        }

        private void ButtonService_Click(object sender, RoutedEventArgs e)
        {
            ShowFrame("Список услуг", new ServiceFrame(connection));

            if (user.Role == Role.Admin)
            {
                addButton.Visibility = Visibility.Visible;
            }

            ExitButton.Visibility = Visibility.Collapsed;
        }

        private void ButtonProfil_Click(object sender, RoutedEventArgs e)
        {
            ShowFrame("Профиль", new Profil(1, connection));

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
            ShowFrame("История действий", new History());

            addButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Collapsed;
        }

        private void ButtonPurchases_Click(object sender, RoutedEventArgs e)
        {
            ShowFrame("Купленные курсы", new Purchases(connection));

            addButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Collapsed;
        }

        private void ShowFrame(string Text, Page page)
        {
            FrameText.Text = Text;
            FrameList.Navigate(page);
        }
    }
}
