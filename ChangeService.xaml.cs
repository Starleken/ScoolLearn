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
        private IConnection connection;

        private Service service;

        private IHistoryHandler history;

        public ChangeService(Service service, IConnection connection, IHistoryHandler history)
        {
            InitializeComponent();
;
            this.connection = connection;

            this.service = service;

            this.history = history;

            FillTextBox();
        }

        private void FillTextBox()
        {
            nameTextBox.Text = service.Title;
            priceTextBox.Text = service.Cost.ToString().Replace('.',',');
            discountTextBox.Text = service.Discount.ToString().Replace('.',',');
            timeTextBox.Text = service.DurationInSeconds.ToString();
            imageTextBox.Text = service.ImagePath.ToString();
        }

        private void CheckFillingTextBox()
        {
            if (nameTextBox.Text == "" || priceTextBox.Text == "" ||
                discountTextBox.Text == "" || timeTextBox.Text == "" || imageTextBox.Text == "")
            {
                throw new DataNotFilledException("Введите данные");
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckFillingTextBox();

                IUpdater updater = new SQLDatabaseUpdater(connection);

                UpdateDataInService();

                updater.UpdateService(service);

                history.AddHistory($"Обновление услуги: '{service.Title}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Close();
        }

        private void UpdateDataInService()
        {
            try
            {
                service.Title = nameTextBox.Text;
                service.Cost = Convert.ToDouble(priceTextBox.Text);
                service.Discount = Convert.ToDouble(discountTextBox.Text);
                service.DurationInSeconds = Convert.ToInt32(timeTextBox.Text);
                service.ImagePath = imageTextBox.Text;
            }
            catch (Exception)
            {
                throw new IncorrectDataException("Некорректные данные");
            }
        }
    }
}
