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
using System.Windows.Shapes;

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy Permission_filter.xaml
    /// </summary>
    public partial class Permission_filter : Window
    {
        public List<string> WybraneUprawnienia { get; private set; } = new List<string>();
        private readonly WarehouseDbContext _context;
        public Permission_filter()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
        }

        private async Task LoadPermissionsAsync()
        {
            // Pobiera wszystkie uprawnienia z bazy danych
            var permissions = await _context.Permissions.ToListAsync();
            PermissionsPanel.ItemsSource = permissions;

            // Zaznacza odpowiednie uprawnienia
            foreach (var item in PermissionsPanel.Items)
            {
                var permission = item as Permission;
                var container = PermissionsPanel.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
            }
        }


        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            await LoadPermissionsAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // zapis filtrów
        {

        }
    }
}
