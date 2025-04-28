using AutoMapper;
using Magazyn.Entities;
using Magazyn.Models;
using Magazyn.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public partial class UsersManager : UserControl
    {
        private WarehouseDbContext _context;
        private IMapper _mapper;
        public UsersManager()
        {
            _context = new WarehouseDbContext();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WarehouseMappingProfile());
            });
            _mapper = config.CreateMapper();
            InitializeComponent();
            this.Loaded += UsersManager_Loaded;
        }
        private async void UsersManager_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshUserDataGrid();
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
                    case "Name":
                        query = query.Where(user => keywords.All(kw => user.FirstName.ToLower().StartsWith(kw) || user.LastName.ToLower().StartsWith(kw)));
                        break;
                    case "Login":
                        query = query.Where(user => keywords.All(kw => user.Login.ToLower().StartsWith(kw)));
                        break;
                    case "E-mail":
                        query = query.Where(user => keywords.All(kw => user.Email.ToLower().StartsWith(kw)));
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


        private async void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            var oknoDodajUser = new AddUser();
            oknoDodajUser.ShowDialog(); // Otwiera nowe okno modalnie
            await RefreshUserDataGrid();
        }

        private async void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            var wybranyUser = UserDataGrid.SelectedItem as User;
            if (wybranyUser == null)
            {
                MessageBox.Show("Proszę wybrać użytkownika do edycji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new ModifyUser(wybranyUser, this);
            editWindow.ShowDialog();
            await RefreshUserDataGrid();

            //UserPreview userPreview = new UserPreview();
            //ContentArea.Content = userPreview;


        }

        private async void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            await RefreshUserDataGrid(TextBoxSearch.Text);
            PlaceholderText.Visibility = string.IsNullOrEmpty(TextBoxSearch.Text)
            ? Visibility.Visible
            : Visibility.Collapsed;
        }

        private void ButtonPreview_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserDataGrid.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Proszę wybrać użytkownika.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var podgla = new UserPreview(selectedUser,this);
            podgla.ShowDialog();
        }

        private async Task ForgetUser(int userId)
        {
            var selectedUser = await _context.Users.Include(u => u.Address).FirstAsync(e => e.Id == userId);
            selectedUser.IsForgotten = true;
            DataRandomizer randomizer = new DataRandomizer();
            selectedUser.FirstName = randomizer.GenerateRandomString(10);
            selectedUser.LastName = randomizer.GenerateRandomString(10);
            selectedUser.DateOfBirth = randomizer.GenerateRandomDateOfBirth();
            selectedUser.Gender = randomizer.GenerateRandomGender();
            selectedUser.PESEL = randomizer.GenerateValidPesel(selectedUser.DateOfBirth, selectedUser.Gender);
            selectedUser.UserPermission = null;
            selectedUser.ForgottenDate = DateTime.Now;
            UserValidator uv = new UserValidator(_context);
            if (!uv.Walidacja(_mapper.Map<CreateUserDto>(selectedUser), selectedUser)) return;
            await _context.SaveChangesAsync();
            await RefreshUserDataGrid();
        }

        private async void buttonForget_Click(object sender, RoutedEventArgs e)
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
                await ForgetUser(wybranyUser.Id);
            }
        }
        private void ShowForgottenUsers_Click(object sender, RoutedEventArgs e)
        {
            var forgottenControl = new ForgottenUser();
            ForgottenUserContent.Content = forgottenControl;
            ForgottenUserContent.Visibility = Visibility.Visible;
        }
        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContentArea.Content = null; // lub np. jakiś domyślny UserControl
            }
        }

        private async void SearchCriteriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await RefreshUserDataGrid(TextBoxSearch.Text);
        }
    }
}