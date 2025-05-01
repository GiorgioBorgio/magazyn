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

        public AddPermission(int id)
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
