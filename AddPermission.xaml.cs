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
    /// <summary>
    /// Logika interakcji dla klasy AddPermission.xaml
    /// </summary>
    public partial class AddPermission : Window
    {
        private readonly WarehouseDbContext _context;
        public AddPermission()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPermissionsAsync();
        }

        private async Task LoadPermissionsAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();
            PermissionsPanel.ItemsSource = permissions;
        }


        private void PermissionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedPermission = button?.Tag as Permission;

            if (selectedPermission != null)
            {
                MessageBox.Show($"Wybrano: {selectedPermission.Name}", "Uprawnienie", MessageBoxButton.OK, MessageBoxImage.Information);
                // Możesz tu np. zapisać do bazy przypisanie roli do użytkownika
            }
        }
    }
}
