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
    public partial class Purchases : Page
    {
        static readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();

        SqlConnection connection;

        public List<int> purchasesService = new List<int>();

        public Purchases()
        {
            InitializeComponent();

            connection = Connection.GetConnection();

            RefreshPurchases();
        }

        public void AddPurchasesServiceWithId(int id)
        {
            purchasesService.Add(id);
        }

        public void RefreshPurchases()
        {
            connection.Close();

            PurchasesServiceStackPanel.Children.Clear();

            connection.Open();

            for (int i = 0; i < purchasesService.Count; i++)
            {
                string sqlExspression = $"SELECT * FROM [Service] WHERE [ID] = {purchasesService[i]}";

                SqlCommand command = new SqlCommand(sqlExspression,connection);

                SqlDataReader reader = command.ExecuteReader();

                Grid grid = new Grid();

                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });

                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(110) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });

                reader.Read();

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
                    price.Text = $"{Math.Round(Convert.ToDecimal(reader["Cost"]), 2)} с учётом {Convert.ToSingle(reader["Discount"]) * 100}% скидки";
                }
                grid.Children.Add(price);
                Grid.SetRow(price, 1);
                Grid.SetColumn(price, 1);
                Grid.SetColumnSpan(price, 2);

                PurchasesServiceStackPanel.Children.Add(grid);

                reader.Close();
            }

            connection.Close();
        }
    }
}
