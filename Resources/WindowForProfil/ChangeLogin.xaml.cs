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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ChangeLogin : Window
    {
        private IConnection connection;

        private User user;

        public ChangeLogin(User user, IConnection connection)
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

                IReader reader = new SQLDatabaseReader(connection);
                reader.CheckUniquenessUser(LoginTextBox.Text);

                user.Login = LoginTextBox.Text;

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
            if (LoginTextBox.Text == "")
            {
                throw new DataNotFilledException("Заполните данные");
            }
        }
    }
}
