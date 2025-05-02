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
        private readonly int _userId;
        private int userId;
        

        public AddPermission(int userId)
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
            _userId = userId;
        }

        public AddPermission()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
            _userId = userId;
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

      
         private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPermissions = new List<Permission>();

            foreach (var item in PermissionsPanel.Items)
            {
                var container = PermissionsPanel.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container != null)
                {
                    var checkBox = FindVisualChild<CheckBox>(container);
                    if (checkBox != null && checkBox.IsChecked == true && checkBox.Tag is Permission permission)
                    {
                        selectedPermissions.Add(permission);
                    }
                }
            }

            // Istniejące uprawnienia użytkownika
            var existingPermissions = await _context.UserPermissions
                .Where(up => up.UserId == _userId)
                .ToListAsync();

            // Dodaj nowe
            foreach (var permission in selectedPermissions)
            {
                if (!existingPermissions.Any(ep => ep.PermissionId == permission.Id))
                {
                    _context.UserPermissions.Add(new UserPermission
                    {
                        UserId = _userId,
                        PermissionId = permission.Id
                    });
                }
            }

            // Usuń odznaczone
            foreach (var existing in existingPermissions)
            {
                if (!selectedPermissions.Any(sp => sp.Id == existing.PermissionId))
                {
                    _context.UserPermissions.Remove(existing);
                }
            }

            await _context.SaveChangesAsync();
            MessageBox.Show("Uprawnienia zapisane.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
         



        

    private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                    return tChild;

                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }


    }
}
