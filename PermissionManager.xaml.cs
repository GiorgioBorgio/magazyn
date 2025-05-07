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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            LoadPermissionTypes();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContentArea.Content = null; // lub np. jakiś domyślny UserControl
            }
        }


        public async Task RefreshUserDataGrid()
        {
                _context.ChangeTracker.Clear();
                //var keywords = searchText.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //string searchField = GetSelectedSearchField();

                var permissions = _context.UserPermissions.Include(e => e.Permission).ToList();
                var filteredUsers = _context.Users.ToList();
                if (PermissionTypeComboBox.SelectedValue is int selectedPermissionId && selectedPermissionId > 0)
                {
                    //MessageBox.Show("Selected permission ID: " + selectedPermissionId + "Permission ID " + _context.Permissions.FirstOrDefault(e=>e.Id==1).Id);
                    
                    permissions = permissions
                        .Where(e => e.PermissionId == selectedPermissionId)
                        .ToList();
                filteredUsers = filteredUsers.Where(user =>
                permissions.Any(up => up.UserId == user.Id && up.PermissionId == selectedPermissionId))
                .ToList();

            }

            


            UserDataGrid.ItemsSource = filteredUsers.Where(e => !e.IsForgotten);
        }







        //private string GetSelectedSearchField()
        //{
        //    var selectedItem = SearchTypeComboBox.SelectedItem as ComboBoxItem;
        //    return selectedItem?.Tag?.ToString() ?? "FirstName";
        //}

        private async void PermissionManager1_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshUserDataGrid();
        }

        //private async void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    await RefreshUserDataGrid();
        //    PlaceholderText.Visibility = string.IsNullOrEmpty(TextBoxSearch.Text)
        //    ? Visibility.Visible
        //    : Visibility.Collapsed;
        //}

        private async void button_zarzadzaj_Click(object sender, RoutedEventArgs e)
        {
           

            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                var okno_dodaj_uprawnienie = new AddPermission(selectedUser.Id);
                okno_dodaj_uprawnienie.ShowDialog();
                await RefreshUserDataGrid();
            }
            else
            {
                MessageBox.Show("Wybierz użytkownika, aby zarządzać uprawnieniami.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //private async void button_filtr_u_Click(object sender, RoutedEventArgs e)
        //{
        //    var okno_filtruj_u = new Permission_filter();
        //    okno_filtruj_u.ShowDialog();
        //    await RefreshUserDataGrid();
        //}

        private void LoadPermissionTypes()
        {

            var permissionTypes =  _context.Permissions.ToList();
            permissionTypes.Insert(0, new Permission
            {
                Id = -1,
                Name = "Wszystkie uprawnienia"
            });
            PermissionTypeComboBox.ItemsSource = permissionTypes;
            PermissionTypeComboBox.DisplayMemberPath = "Name";        // widoczne w UI
            PermissionTypeComboBox.SelectedValuePath = "Id";          // np. do filtrowania
            PermissionTypeComboBox.SelectedIndex = 0;
        }

        private void PermissionTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             RefreshUserDataGrid();
        }
        private void ListPermissionsButton_Click(object sender, RoutedEventArgs e)
        {
            var permissions = _context.Permissions
                .OrderBy(p => p.Name)
                .Select(p => p.Name)
                .ToList();

            var permission_window = new PermissionListWindow(permissions);
            permission_window.Owner = Window.GetWindow(this);
            permission_window.ShowDialog();
        }


    }
}
