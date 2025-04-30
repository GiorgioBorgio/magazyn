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
using Magazyn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Magazyn
{

    public partial class PermissionWindow : Window
    {
        private WarehouseDbContext _context;
        public PermissionWindow()
        {
            InitializeComponent();
            this.Loaded += UsersManager_Loaded;


            _context = new WarehouseDbContext();
        }

        private async void UsersManager_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshUserDataGrid();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Wywołanie metody, która pobierze dane użytkowników
            await LoadUsersAsync();
        }


        private async Task LoadUsersAsync()
        {
            // Pobieramy wszystkich użytkowników z bazy danych, którzy nie są zapomniani
            var users = await _context.Users
                                      .Where(u => u.IsForgotten == false) // Tylko aktywni użytkownicy
                                      .ToListAsync(); // Wykonanie zapytania i pobranie wyników

            // Ustawiamy dane w DataGrid
            UserDataGrid.ItemsSource = users;
        }

        public async Task RefreshUserDataGrid(string searchText = "")
        {
            _context.ChangeTracker.Clear();
            var keywords = searchText.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string searchField = GetSelectedSearchField();

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                switch (searchField)
                {
                    case "Imię":
                        query = query.Where(user => keywords.All(kw => user.FirstName.ToLower().StartsWith(kw) || user.LastName.ToLower().StartsWith(kw)));
                        break;
                    case "Nazwisko":
                        query = query.Where(user => keywords.All(kw => user.LastName.ToLower().StartsWith(kw)));
                        break;
                    case "Login":
                        query = query.Where(user => keywords.All(kw => user.Login.ToLower().StartsWith(kw)));
                        break;
                }
            }

            var filteredUsers = await query.AsNoTracking().ToListAsync();
            UserDataGrid.ItemsSource = filteredUsers.Where(e => e.IsForgotten == false);
        }

        private async void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await RefreshUserDataGrid(TextBoxSearch.Text);
            PlaceholderText.Visibility = string.IsNullOrEmpty(TextBoxSearch.Text)
            ? Visibility.Visible
            : Visibility.Collapsed;
        }

        private string GetSelectedSearchField()
        {
            var selectedItem = SearchTypeComboBox.SelectedItem as ComboBoxItem;
            return selectedItem?.Tag?.ToString() ?? "FirstName";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AssignPermission_Click(object sender, RoutedEventArgs e)
        {
            var addPermissionWindow = new AddPermission();
            addPermissionWindow.ShowDialog();
        }
    }
}
