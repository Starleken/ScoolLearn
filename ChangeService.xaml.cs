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
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            IUpdater updater = new SQLDatabaseUpdater(connection);

            UpdateService();

            updater.UpdateService(service);

            history.AddHistory($"Обновление услуги: '{service.Title}''");
        }

        private void UpdateService()
        {
            service.Cost = Convert.ToDouble(priceTextBox.Text);
        }
    }
}
