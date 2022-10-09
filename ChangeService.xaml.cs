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
using ScoolLearn.Resources.Frames;

namespace ScoolLearn
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class ChangeService : Window
    {
        SqlConnection connection;

        int idService = 0;

        public ChangeService(int id)
        {
            InitializeComponent();

            idService = id;

            FillTextBox();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();

            float discount = 0;

            if (discountTextBox.Text != "")
            {
                discount = Convert.ToSingle(discountTextBox.Text) / 100;
            }
            string discountText = discount.ToString().Replace(',', '.');

            string sqlExpression = $"UPDATE Service SET Title = '{nameTextBox.Text}', Cost = {priceTextBox.Text}, DurationInSeconds = {timeTextBox.Text}, Discount = {discountText} WHERE ID = {idService}";

            SqlCommand cmd = new SqlCommand(sqlExpression, connection);

            cmd.ExecuteNonQuery();

            connection.Close();

            Service.GetInstance().RefreshCervice();

            MainMenu.history.AddHistory($"Изменена услуга {nameTextBox.Text}");

            this.Close();
        }

        private async void FillTextBox()
        {
            connection = Connection.GetConnection();

            connection.Open();

            string sqlExpression = $"SELECT * FROM Service WHERE ID = {idService}";

            SqlCommand cmd = new SqlCommand(sqlExpression, connection);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            reader.Read();

            nameTextBox.Text = reader["Title"].ToString();
            priceTextBox.Text = reader["Cost"].ToString().Replace(',','.');
            timeTextBox.Text = reader["DurationInSeconds"].ToString();
            discountTextBox.Text = (Convert.ToSingle(reader["Discount"]) * 100).ToString();
            imageTextBox.Text = reader["MainImagePath"].ToString();

            connection.Close();
        }
    }
}
