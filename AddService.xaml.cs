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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddService : Window
    {
        private SqlConnection connection;

        public AddService(SqlConnection connection)
        {
            InitializeComponent();

            this.connection = connection;
        }

        private  void addButton_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();

            if (CheckFilling()) { }
            else
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите добавить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            float discount = 0;

            if (discountTextBox.Text != "")
            {
                discount = Convert.ToSingle(discountTextBox.Text) / 100;
            }
            string discountText = discount.ToString().Replace(',', '.');

            string sqlCommand = $"INSERT INTO Service (Title, Cost, DurationInSeconds, Discount, MainImagePath) VALUES ('{nameTextBox.Text}', {priceTextBox.Text}, {timeTextBox.Text}, {discountText}, '{imageTextBox.Text}')";

            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            cmd.ExecuteNonQuery();

            connection.Close();

            Service.GetInstance().RefreshCervice();

            MainMenu.history.AddHistory($"Добавлена услуга {nameTextBox.Text}");
        }

        private bool CheckFilling()
        {
            if (nameTextBox.Text != "" && priceTextBox.Text != "" && timeTextBox.Text != null)
            {
                return true;
            }
            return false;
        }
    }
}
