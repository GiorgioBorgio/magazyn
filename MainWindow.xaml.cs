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
            InitializeComponent();
            context = new WarehouseDbContext();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsersManager usersManager = new UsersManager();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ForgottenUser forgottenUsers = new ForgottenUser();
        }
        private void OpenUsersManager(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new UsersManager();
        }
        private void OpenPermissionManager(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new PermissionManager();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Czy na pewno chcesz się wylogować?",
                "Wylogowanie",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                
                var loginWindow = new Login_window();

                
                loginWindow.Show();

                
                this.Close();
            }
        }

    }
}