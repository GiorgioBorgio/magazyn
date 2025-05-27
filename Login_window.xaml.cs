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

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy Login_window.xaml
    /// </summary>
    public partial class Login_window : Window
    {
        public Login_window()
        {
            InitializeComponent();
        }

        private void Button_logowanie_ok_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password; // Jeśli TextBox, użyj .Text

            using (var context = new Entities.WarehouseDbContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user != null)
                {
                    MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);


                    // Otwórz nowe okno
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    // Zamknij to okno logowania
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Błędny login lub hasło.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
