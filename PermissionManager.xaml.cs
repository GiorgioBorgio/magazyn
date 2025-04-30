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
        private PermissionWindow _permissionWindow;
        public PermissionManager()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NadajUprawnienia_Click(object sender, RoutedEventArgs e)
        {
            if (_permissionWindow == null || !_permissionWindow.IsLoaded)
            {
                _permissionWindow = new PermissionWindow();
                _permissionWindow.Show();
            }
            else
            {
                _permissionWindow.Activate(); // przywróć okno na wierzch, jeśli już otwarte
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void PermissionManager1_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPermissionsAsync();
        }

        private async Task LoadPermissionsAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();
            UserDataGrid.ItemsSource = permissions;
        }
    }
}
