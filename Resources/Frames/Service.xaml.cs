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
    public partial class Service : Page
    {
        SqlConnection connection;

        static readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();

        static public Service instance;

        public Service()
        {
            InitializeComponent();

            connection = Connection.GetConnection();

            instance = this;

            CheckPurchases();

            RefreshCervice();
        }

        public static Service GetInstance()
        {
            return instance;
        }

        public void RefreshCervice()
        {
            connection.Close();

            connection.Open();

            string sqlExpression = "SELECT * FROM Service";

            SqlCommand cmd = new SqlCommand(sqlExpression, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            serviceStackPanel.Children.Clear();

            //Создание одной услуги из базы данных
            while (reader.Read())
            {
                Grid grid = new Grid();

                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });

                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(110) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });

                Image image = new Image();
                try
                {
                    image.Source = (ImageSource)imageSourceConverter.ConvertFrom(reader["MainImagePath"].ToString().Trim());
                }
                catch (Exception) { }
                grid.Children.Add(image);
                Grid.SetRowSpan(image, 2);

                TextBlock id = new TextBlock();
                id.Text = reader["id"].ToString();
                id.FontSize = 10;
                id.VerticalAlignment = VerticalAlignment.Center;
                grid.Children.Add(id);
                Grid.SetRow(id, 0);
                Grid.SetColumn(id, 1);
                Grid.SetColumnSpan(id, 3);
                id.Visibility = Visibility.Collapsed;

                TextBlock name = new TextBlock();
                name.Text = (string)reader["Title"];
                name.FontSize = 13;
                grid.Children.Add(name);
                Grid.SetRow(name, 0);
                Grid.SetColumn(name, 1);
                Grid.SetColumnSpan(name, 3);

                TextBlock price = new TextBlock();
                if (Convert.ToDouble(reader["Discount"]) == 0)
                {
                    price.Text = $"{Math.Round(Convert.ToDecimal(reader["Cost"]), 2)} рублей";
                }
                else
                {
                    price.Text = $"{ Math.Round(Convert.ToDecimal(reader["Cost"]), 2)} с учётом {Convert.ToSingle(reader["Discount"]) * 100}% скидки";
                }
                grid.Children.Add(price);
                Grid.SetRow(price, 1);
                Grid.SetColumn(price, 1);
                Grid.SetColumnSpan(price, 2);

                if (Role.RoleLevel == 1)
                {
                    Button editing = new Button();
                    editing.Content = "Редактировать";
                    grid.Children.Add(editing);
                    Grid.SetRow(editing, 2);
                    Grid.SetColumn(editing, 1);
                    editing.Margin = new Thickness(5);
                    editing.Click += ChangeButton_Click;

                    Button delete = new Button();
                    delete.Content = "Удалить";
                    grid.Children.Add(delete);
                    Grid.SetRow(delete, 2);
                    Grid.SetColumn(delete, 2);
                    delete.Margin = new Thickness(5);
                    delete.Click += DeleteButton_Click;
                }
                else if(Role.RoleLevel == 2)
                {
                    Button checkClient = new Button();
                    checkClient.Content = "Клиенты";
                    grid.Children.Add(checkClient);
                    Grid.SetRow(checkClient, 2);
                    Grid.SetColumn(checkClient, 1);
                    checkClient.Margin = new Thickness(5);
                    checkClient.Click += CheckClient_Click;
                }
                else if (Role.RoleLevel == 3)
                {
                    if (MainMenu.purchases.purchasesService.Contains(Convert.ToInt32(id.Text)))
                    {
                        Button buy = new Button();
                        buy.Content = "Куплено";
                        grid.Children.Add(buy);
                        Grid.SetRow(buy, 2);
                        Grid.SetColumn(buy, 1);
                        buy.Margin = new Thickness(5);
                        buy.Click += Buy_Click;
                        buy.IsEnabled = false;
                    }
                    else
                    {
                        Button buy = new Button();
                        buy.Content = "Купить";
                        grid.Children.Add(buy);
                        Grid.SetRow(buy, 2);
                        Grid.SetColumn(buy, 1);
                        buy.Margin = new Thickness(5);
                        buy.Click += Buy_Click;
                    }
                }

                serviceStackPanel.Children.Add(grid);
            }

            connection.Close();
        }

        private void CheckPurchases()
        {
            connection.Open();

            string sqlExspression = $"SELECT * FROM [User] WHERE [ID] = {Role.UserId}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();
            string middleName = reader["MiddleName"].ToString();

            connection.Close();

            int id = CheckIdentifityByName(firstName, lastName, middleName);

            connection.Open();

            if (id == -1)
            {
                return;
            }

            connection.Close();

            SetPurchases(id);
        }

        private void SetPurchases(int clientId)
        {
            connection.Open();

            string sqlExspression = $"SELECT [ServiceID] FROM [ClientService] WHERE [ClientID] = {clientId}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MainMenu.purchases.AddPurchasesServiceWithId(Convert.ToInt32(reader["ServiceID"]));
            }
            MainMenu.purchases.RefreshPurchases();

            connection.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =  MessageBox.Show("Вы уверены, что хотите удалить услугу?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            int id = FindIdService(sender);

            connection.Open();

            string sqlExspression = $"SELECT Title FROM [Service] WHERE [Id] = {id}";

            SqlCommand firstCmd = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = firstCmd.ExecuteReader();

            reader.Read();

            MainMenu.history.AddHistory($"Удалена услуга {reader["Title"]}");

            connection.Close();

            connection.Open();

            SqlCommand cmd = new SqlCommand($"DELETE  FROM Service WHERE Id={id}", connection);

            cmd.ExecuteNonQuery();

            connection.Close();

            RefreshCervice();

        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeService changeService = new ChangeService(FindIdService(sender));

            changeService.Show();
        }

        private void CheckClient_Click(object sender, RoutedEventArgs e)
        {

            ListClient listClient = new ListClient(FindIdService(sender));

            listClient.Show();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
         {  
            MainMenu.purchases.AddPurchasesServiceWithId(FindIdService(sender));
            MainMenu.purchases.RefreshPurchases();

            RefreshCervice();

            connection.Open();

            string sqlExspression = $"SELECT * FROM [User] WHERE [ID] = {Role.UserId}";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();
            string middleName = reader["MiddleName"].ToString();

            connection.Close();

            if (CheckIdentifityByName(firstName, lastName, middleName) == -1)
            {
                AddClient();
            }

            sqlExspression = $"INSERT INTO ClientService (ClientID, ServiceID, StartTime) VALUES ({CheckIdentifityByName(firstName, lastName, middleName)}, {FindIdService(sender)}, GetDate())";

            connection.Open();

            SqlCommand command2 = new SqlCommand(sqlExspression, connection);

            command2.ExecuteNonQuery();

            connection.Close();
        }

        private void AddClient()
        {
            connection.Open();

            string sqlExsperssion = $"SELECT * FROM [User] WHERE [ID] = {Role.UserId}";

            SqlCommand command = new SqlCommand(sqlExsperssion, connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            string sqlExspression = $"INSERT INTO Client (FirstName, LastName, Patronymic, Birthday, RegistrationDate, Email, Phone, GenderCode, PhotoPath) VALUES " +
                $"('{reader["lastName"]}', '{reader["FirstName"]}', '{reader["MiddleName"]}', GetDate(), GetDate(), '{reader["Email"]}', {reader["Phone"]}, '{reader["GenderCode"]}', '{reader["PhotoPath"]}')";

            reader.Close();

            SqlCommand command1 = new SqlCommand(sqlExspression, connection);

            command1.ExecuteNonQuery();

            connection.Close();
        }

        private int CheckIdentifityByName(string lastName, string firstName, string middleName)
        {
            connection.Open();

            string sqlExspression = $"SELECT * FROM [Client] WHERE [LastName] = '{lastName}' AND [FirstName] = '{firstName}'";

            SqlCommand command = new SqlCommand(sqlExspression, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                int id = Convert.ToInt32(reader["ID"]);

                connection.Close();

                return id;
            }
            else
            {
                connection.Close();

                return -1;
            }
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
