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
using ScoolLearn.Resources.Scripts.Factory;

namespace ScoolLearn
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddService : Window
    {
        private IConnection connection;
        private IHistoryHandler history;

        public AddService(IConnection connection, IHistoryHandler history)
        {
            InitializeComponent();

            this.connection = connection;
            this.history = history;
        }

        private  void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckFilling();

                Service service = new ServiceFactory().Get(nameTextBox.Text, Convert.ToDouble(priceTextBox.Text), Convert.ToInt32(timeTextBox.Text), Convert.ToDouble(discountTextBox.Text), imageTextBox.Text, null);

                IAdder adder = new SQLDatabaseAdder(connection);
                adder.AddService(service);

                history.AddHistory($"Добавление услуги: {service.Title}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Close();
        }

        private void CheckFilling()
        {
            if (nameTextBox.Text == "" || priceTextBox.Text == "" || timeTextBox.Text == "" || discountTextBox.Text == "" || imageTextBox.Text == "")
            {
                throw new DataNotFilledException("Введите данные");
            }
        }
    }
}
