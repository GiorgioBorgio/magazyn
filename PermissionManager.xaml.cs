using Magazyn.Entities;
using Microsoft.EntityFrameworkCore;
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

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy PermissionManager.xaml
    /// </summary>
    public partial class PermissionManager : UserControl
    {
        private WarehouseDbContext _context;
        public PermissionManager()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContentArea.Content = null; // lub np. jakiś domyślny UserControl
            }
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
        private string GetSelectedSearchField()
        {
            var selectedItem = SearchTypeComboBox.SelectedItem as ComboBoxItem;
            return selectedItem?.Tag?.ToString() ?? "FirstName";
        }

        private async void PermissionManager1_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshUserDataGrid();
        }
    }
}
