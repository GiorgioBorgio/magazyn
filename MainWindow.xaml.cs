using Magazyn.Entities;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magazyn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WarehouseDbContext context;
        public MainWindow()
        {
            context = new WarehouseDbContext();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsersManager usersManager = new UsersManager();
            usersManager.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ForgottenUser forgottenUsers = new ForgottenUser();
            forgottenUsers.Show();
        }
    }
}