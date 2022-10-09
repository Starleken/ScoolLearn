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
        public class Client
        {
            public string id { get; set; }
            public string lastName { get; set; }
            public string firstName { get; set; }
            public string patronymic { get; set; }
            public string EMail { get; set; }
            public string telephone { get; set; }
        }

        private IConnection connection;

        private int serviceId;

        private List<Client> users = new List<Client>();

        public ListClient(int serviceId, IConnection connection)
        {
            InitializeComponent();

            this.serviceId = serviceId;

            this.connection = connection;

            SetNameService();

            RefreshClient();
        }

        private void RefreshClient()
        {
            throw new Exception();
        }

        private void SetNameService()
        {
            throw new Exception();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }
    }
}
