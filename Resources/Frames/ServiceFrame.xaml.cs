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

namespace ScoolLearn.Resources.Frames
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class ServiceFrame : Page
    {
        private IConnection connection;

        static readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();

        private List<Service> services;

        public ServiceFrame(IConnection connection)
        {
            InitializeComponent();

            this.connection = connection;

            RefreshCervice();

            ServicesList.ItemsSource = services;
        }

        public void RefreshCervice()
        {
            services = new SQLDatabaseReader(connection).ReadServices();
        }
       
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IDeleter deleter = new SQLDatabaseDeleter(connection);

            try
            {
                deleter.DeleteObject((Service)ServicesList.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeService changeService = new ChangeService(FindIdService(sender));

            changeService.Show();
        }

        private void CheckClient_Click(object sender, RoutedEventArgs e)
        {

            ListClient listClient = new ListClient(FindIdService(sender), connection);

            listClient.Show();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void AddClient()
        {
            throw new Exception();
        }

        private int CheckIdentifityByName(string lastName, string firstName, string middleName)
        {
            throw new Exception();
        }

        private int FindIdService(object sender)
        {
            Button button = (Button)sender;

            Grid grid = (Grid)button.Parent;

            TextBlock idBlock = (TextBlock)grid.Children[1];

            int id = Convert.ToInt32(idBlock.Text);

            return id;
        }
    }
}
