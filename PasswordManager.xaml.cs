using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Magazyn.Entities;

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy PasswordManager.xaml
    /// </summary>
    public partial class PasswordManager : UserControl
    {
        public PasswordManager()
        {
            InitializeComponent();
            this.Loaded += PasswordManager_Loaded; // Upewnij się, że metoda jest przypięta
        }

        // Obsługa przycisku cofania (jeśli potrzebna)
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Możesz tu dodać logikę powrotu do poprzedniego widoku
        }

        // Odświeżenie listy użytkowników
        private async Task RefreshUserDataGrid()
        {
            using (var context = new WarehouseDbContext())
            {
                var users = await Task.Run(() => context.Users.ToList());
                UserDataGrid.ItemsSource = users;
            }
        }

        // Obsługa kliknięcia przycisku "Zarządzaj"
        private async void button_zarzadzaj_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                using (var context = new WarehouseDbContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Id == selectedUser.Id);
                    if (user == null) return;

                    var passwordHistory = context.PasswordHistories
                        .Where(ph => ph.UserId == selectedUser.Id)
                        .OrderByDescending(ph => ph.ChangeDate) // zakładam, że masz pole daty
                        .Take(3)
                        .Select(ph => ph.Password)
                        .ToList();

                    var passwordChangeWindow = new PasswordChangeWindow(user, passwordHistory);
                    passwordChangeWindow.ShowDialog();
                    await RefreshUserDataGrid(); // po zamknięciu okna odśwież listę
                }
            }
        }

        // Załaduj dane przy starcie kontrolki
        private async void PasswordManager_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshUserDataGrid();
        }
    }
}
