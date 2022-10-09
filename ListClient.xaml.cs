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

        private SqlConnection connection;

        private int serviceId;

        private List<Client> users = new List<Client>();

        public ListClient(int serviceId)
        {
            InitializeComponent();

            this.serviceId = serviceId;

            connection = Connection.GetConnection();

            SetNameService();

            RefreshClient();
        }

        private void RefreshClient()
        {
            ClientDataGrid.Items.Clear();

            users.Clear();

            connection.Open();

            string sqlExspression = $"SELECT ClientID FROM [ClientService] WHERE [ServiceId] = {serviceId}";

            SqlCommand cmd = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            List<int> clientId = new List<int>();

            while (reader.Read())
            {
                clientId.Add(Convert.ToInt32(reader["ClientID"]));
            }

            reader.Close();

            for (int i = 0; i < clientId.Count; i++)
            {
                sqlExspression = $"SELECT * FROM [Client] WHERE [ID] = {clientId[i]}";

                cmd = new SqlCommand(sqlExspression, connection);

                reader = cmd.ExecuteReader();

                reader.Read();

                Client client = new Client()
                {
                    id = reader["ID"].ToString(),
                    firstName = reader["FirstName"].ToString(),
                    lastName = reader["LastName"].ToString(),
                    patronymic = reader["Patronymic"].ToString(),
                    EMail = reader["Email"].ToString(),
                    telephone = reader["Phone"].ToString()
                };

                reader.Close();

                ClientDataGrid.Items.Add(client);

                users.Add(client);
            }

            connection.Close();
        }

        private void SetNameService()
        {
            connection.Open();

            string sqlExspression = $"SELECT Title FROM [Service] WHERE [ID] = {serviceId}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            ServiceNameTextBlock.Text = $"{reader["Title"]}";

            connection.Close();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите пользователя","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            MessageBoxResult result =  MessageBox.Show("Вы уверены?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            connection.Open();

            int index = ClientDataGrid.SelectedIndex;

            string sqlExspression = $"DELETE FROM [ClientService] WHERE [ClientId] = {users[index].id} AND [ServiceId] = {serviceId}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            command.ExecuteNonQuery();

            connection.Close();

            ClientDataGrid.Items.Remove(users[index]);
        }
    }
}
