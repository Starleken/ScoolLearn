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

namespace ScoolLearn
{
    /// <summary>
    /// Логика взаимодействия для листа клиентов.xaml
    /// </summary>
    public partial class ListClient : Window
    {
        private IConnection connection;

        private Service service;

        private List<Client> humans = new List<Client>();

        public ListClient(Service service, IConnection connection)
        {
            InitializeComponent();

            this.service = service;

            this.connection = connection;

            RefreshClient();

            ServiceNameTextBlock.Text = service.Title;
        }

        private void RefreshClient()
        {
            IReader reader = new SQLDatabaseReader(connection);
            humans = reader.GetClientsByService(service);

            foreach (Client client in humans)
            {
                ClientDataGrid.Items.Add(client);
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }
    }
}
