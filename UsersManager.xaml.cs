using Magazyn.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UsersMenager.xaml
    /// </summary>
    public partial class UsersManager : Window
    {
        private WarehouseDbContext _context;
        private List<User> _users = new List<User>();
        public UsersManager()
        {
            _context = new WarehouseDbContext();
            InitializeComponent();
            RefreshUserDataGrid();
        }

        public void RefreshUserDataGrid(string searchText = "")
        {
            _context.ChangeTracker.Clear();
            var keywords = searchText.Split(' ');
            UserDataGrid.ItemsSource = null; // Resetujemy źródło
            var filteredUsers = _context.Users
                .Where(user =>
        keywords.All(kw =>
            user.FirstName.ToLower().Contains(kw) ||
            user.LastName.ToLower().Contains(kw) ||
            user.Login.ToLower().Contains(kw) ||
            user.PESEL.Contains(kw))).AsNoTracking().ToList();
            _users = new List<User>(filteredUsers);
            UserDataGrid.ItemsSource = _users.Where(e=>e.IsForgotten == false);
        }


        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            var oknoDodajUser = new AddUser();
            oknoDodajUser.ShowDialog(); // Otwiera nowe okno modalnie
            RefreshUserDataGrid();
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            var wybranyUser = UserDataGrid.SelectedItem as User;
            if (wybranyUser == null)
            {
                MessageBox.Show("Proszę wybrać użytkownika do edycji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new ModifyUser(wybranyUser, this);
            editWindow.ShowDialog();
            RefreshUserDataGrid();
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshUserDataGrid(TextBoxSearch.Text);
            PlaceholderText.Visibility = string.IsNullOrEmpty(TextBoxSearch.Text)
            ? Visibility.Visible
            : Visibility.Collapsed;
        }

        private void UserDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = UserDataGrid.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Proszę wybrać użytkownika.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var podgla = new UserPreview(selectedUser);
            podgla.ShowDialog();
        }

        private void ForgetUser(int userId)
        {
            var selectedUser = _context.Users.FirstOrDefault(e=>e.Id == userId);
            selectedUser.IsForgotten = true;
            DataRandomizer randomizer = new DataRandomizer();
            selectedUser.FirstName = randomizer.GenerateRandomString(10);
            selectedUser.LastName = randomizer.GenerateRandomString(10);
            selectedUser.DateOfBirth = randomizer.GenerateRandomDateOfBirth();
            selectedUser.PESEL = randomizer.GenerateValidPesel(selectedUser.DateOfBirth);
            selectedUser.Gender = null;
            selectedUser.UserPermission = null;
            selectedUser.ForgottenDate = DateTime.Now;
            
            _context.SaveChanges();
            RefreshUserDataGrid();
        }

        private void buttonForget_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zapomieć tego użytkownika?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var wybranyUser = UserDataGrid.SelectedItem as User;
                if (wybranyUser == null)
                {
                    MessageBox.Show("Proszę wybrać użytkownika do edycji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ForgetUser(wybranyUser.Id);
            }
            else return;

        }
    }
}
