using Magazyn.Entities;
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
    /// Interaction logic for UsersMenager.xaml
    /// </summary>
    public partial class UsersMenager : Window
    {
        private WarehouseDbContext _context;
        public UsersMenager()
        {
            _context = new WarehouseDbContext();
            InitializeComponent();
            RefreshUserDataGrid();
        }


        private void Button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            var oknoDodajUser = new AddModifyUser();
            oknoDodajUser.ShowDialog(); // Otwiera nowe okno modalnie
            RefreshUserDataGrid();
        }
        private void RefreshUserDataGrid()
        {
            UserDataGrid.ItemsSource = null; // Resetujemy źródło
            UserDataGrid.ItemsSource = _context.Users.ToList(); // Ładujemy dane z listy User
        }

        private void Button_edycja_Click_1(object sender, RoutedEventArgs e)
        {
            var wybranyUser = UserDataGrid.SelectedItem as User;
            if (wybranyUser == null)
            {
                MessageBox.Show("Proszę wybrać użytkownika do edycji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new AddModifyUser(wybranyUser);
            editWindow.ShowDialog();
            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = _context.Users.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wybranyUser = UserDataGrid.SelectedItem as User;
            if (wybranyUser == null)
            {
                MessageBox.Show("Proszę wybrać użytkownika do edycji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new AddModifyUser(wybranyUser);
            editWindow.ShowDialog();
            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = _context.Users.ToList();
        }
    }
}
