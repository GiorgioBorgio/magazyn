using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        public AddPermission(int userId)
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
            _userId = userId;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Załaduj uprawnienia użytkownika z bazy
            await LoadPermissionsAsync();
        }

        // Załadowanie dostępnych uprawnień
        private async Task LoadPermissionsAsync()
        {
            // Pobierz wszystkie uprawnienia z bazy danych
            var permissions =  _context.Permissions.ToList();
            PermissionsPanel.ItemsSource = permissions;

            var userPermissionIds =  _context.UserPermissions
                .Where(up => up.UserId == _userId)
                .Select(up => up.PermissionId)
                .ToList();

            // Pobierz istniejące uprawnienia użytkownika
            var userPermissions = _context.UserPermissions
                .Where(up => up.UserId == _userId)
                .Include(up => up.Permission)
                .ToList();

            // Zaznacz odpowiednie uprawnienia
            //await Dispatcher.InvokeAsync(() =>
            //{
            //    foreach (var item in PermissionsPanel.Items)
            //    {
            //        var permission = item as Permission;
            //        var userPermission = userPermissions.FirstOrDefault(up => up.PermissionId == permission?.Id);
            //        var container = PermissionsPanel.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
            //        if (container != null)
            //        {
            //            var checkBox = FindVisualChild<CheckBox>(container);
            //            if (checkBox != null)
            //            {
            //                checkBox.IsChecked = userPermission != null;
            //            }
            //        }
            //    }
            //});

            await Dispatcher.InvokeAsync(() =>
            {
                foreach (var item in PermissionsPanel.Items)
                {
                    if (item is Permission permission)
                    {
                        var container = PermissionsPanel.ItemContainerGenerator.ContainerFromItem(permission) as FrameworkElement;
                        if (container != null)
                        {
                            var checkBox = FindVisualChild<CheckBox>(container);
                            if (checkBox != null)
                            {
                                checkBox.IsChecked = userPermissionIds.Contains(permission.Id);
                                checkBox.Tag = permission; // Upewniamy się, że Tag jest ustawiony
                            }
                        }
                    }
                }

            });
        }

        // Zapisz uprawnienia do bazy danych
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPermissions = new List<Permission>();

            // Przejdź przez wszystkie kontrolki CheckBox i wybierz zaznaczone
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
            bool isChanged = false;

            // Dodaj nowe uprawnienia
            foreach (var permission in selectedPermissions)
            {
                if (!existingPermissions.Any(ep => ep.PermissionId == permission.Id))
                {
                    _context.UserPermissions.Add(new UserPermission
                    {
                        UserId = _userId,
                        PermissionId = permission.Id
                    });
                    //isChanged = IsPermissionChanged(existingPermissions, selectedPermissions);
                }
            }

            // Usuń odznaczone uprawnienia
            foreach (var existing in existingPermissions)
            {
                if (!selectedPermissions.Any(sp => sp.Id == existing.PermissionId))
                {
                    _context.UserPermissions.Remove(existing);
                    //isChanged = true;
                }
            }
            isChanged = IsPermissionChanged(existingPermissions, selectedPermissions);

            // Zapisz zmiany w bazie danych
            if (isChanged)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zapisać zmiany?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await _context.SaveChangesAsync();
                }
            }
            this.Close();
        }

        // Pomocnicza funkcja do znalezienia kontrolki CheckBox w wizualnym drzewie
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
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPermissions = new List<Permission>();

            // Przejdź przez wszystkie kontrolki CheckBox i wybierz zaznaczone
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
            var existingPermissions = _context.UserPermissions
               .Where(up => up.UserId == _userId)
               .ToList();
            bool isChanged = false;
            isChanged = IsPermissionChanged(existingPermissions, selectedPermissions);
            if (isChanged)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz anulować zmiany?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.SaveChangesAsync();
                }
            }
            this.Close();
        }
        private bool IsPermissionChanged(List<UserPermission> existingPermissions, List<Permission> selectedPermissions)
        {
            bool isChanged = false;
            foreach (var permission in selectedPermissions)
            {
                if (!existingPermissions.Any(ep => ep.PermissionId == permission.Id))
                {
                    isChanged = true;
                }
            }

            foreach (var existing in existingPermissions)
            {
                if (!selectedPermissions.Any(sp => sp.Id == existing.PermissionId))
                {
                    isChanged = true;
                }
            }
            return isChanged;
        }

    }
}
