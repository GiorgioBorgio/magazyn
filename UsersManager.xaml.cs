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
    public partial class UsersManager : Window
    {
        private WarehouseDbContext _context;
        public UsersManager()
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
        private void RefreshUserDataGrid(string searchText = "")
        {
            UserDataGrid.ItemsSource = null; // Resetujemy źródło
            UserDataGrid.ItemsSource = _context.Users.Where(e => ((e.FirstName.Contains(searchText) || e.Login.Contains(searchText)
             || e.PESEL.Contains(searchText) || e.LastName.Contains(searchText)) && (e.IsForgotten == false))).ToList();
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

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            var oknoDodajUser = new AddModifyUser();
            oknoDodajUser.ShowDialog(); // Otwiera nowe okno modalnie
            RefreshUserDataGrid();
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
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

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshUserDataGrid(TextBoxSearch.Text);
        }

    }
}
