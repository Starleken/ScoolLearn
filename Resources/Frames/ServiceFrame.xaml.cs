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

        private List<Service> services;

        private IHistoryHandler history;

        public ServiceFrame(IConnection connection, IHistoryHandler history)
        {
            InitializeComponent();

            this.connection = connection;

            RefreshCervice();

            ServicesList.ItemsSource = services;

            this.history = history;
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
            ChangeService changeService = new ChangeService((Service)ServicesList.SelectedItem, connection, history);

            changeService.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ChangeService changeService = new ChangeService((Service)ServicesList.SelectedItem, connection, history);
            changeService.ShowDialog();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListClient listClient = new ListClient((Service)ServicesList.SelectedItem, connection);

            listClient.ShowDialog();
        }
    }
}
