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
    /// Logika interakcji dla klasy DeletedUsers.xaml
    /// </summary>
    public partial class ForgottenUser : UserControl
    {
        private WarehouseDbContext _context;
        private List<User> _users;
        public ForgottenUser()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
            ForgottenDataGrid.ItemsSource = _context.Users.Where(e => e.IsForgotten == true).ToList();
        }

        public void RefreshUserDataGrid(string searchText = "")
        {
            _context.ChangeTracker.Clear();
            var keywords = searchText.Split(' ');
            ForgottenDataGrid.ItemsSource = null; // Resetujemy źródło
            var filteredUsers = _context.Users
                .Where(user =>
            keywords.All(kw =>
            user.FirstName.ToLower().Contains(kw) ||
            user.LastName.ToLower().Contains(kw) ||
            user.Login.ToLower().Contains(kw) ||
            user.PESEL.Contains(kw))).AsNoTracking().ToList();
            _users = new List<User>(filteredUsers);
            ForgottenDataGrid.ItemsSource = _users.Where(e => e.IsForgotten == true);
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshUserDataGrid(TextBoxSearch.Text);
            PlaceholderText.Visibility = string.IsNullOrEmpty(TextBoxSearch.Text)
            ? Visibility.Visible
            : Visibility.Collapsed;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContentArea.Content = new UsersManager();
            }
        }

    }
}