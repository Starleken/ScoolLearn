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

namespace ScoolLearn.Resources.WindowForProfil
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class ChangeMail : Window
    {
        private IConnection connection;

        private User user;

        public ChangeMail(User user, IConnection connection)
        {
            InitializeComponent();

            this.user = user;

            this.connection = connection;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckFilling();

                user.Email = MailTextBox.Text;

                IUpdater updater = new SQLDatabaseUpdater(connection);
                updater.UpdateUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }

        private void CheckFilling()
        {
            if (MailTextBox.Text == "")
            {
                throw new DataNotFilledException("Введите Данные");
            }
        }
    }
}
