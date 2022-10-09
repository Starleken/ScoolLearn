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

        private IConnection connection;

        public List<int> purchasesService = new List<int>();

        public Purchases(IConnection connection)
        {
            InitializeComponent();

            this.connection = connection;

            RefreshPurchases();
        }

        public void AddPurchasesServiceWithId(int id)
        {
            purchasesService.Add(id);
        }

        public void RefreshPurchases()
        {
            throw new Exception();
        }
    }
}
