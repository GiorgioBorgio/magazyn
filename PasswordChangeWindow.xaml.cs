using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Magazyn.Entities;

namespace Magazyn
{
    public partial class PasswordChangeWindow : Window
    {
        private readonly User _user;

        internal PasswordChangeWindow(User user)
        {
            InitializeComponent();
            _user = user;
            LoginTextBox.Text = _user.Login;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordBox.Password;

            using (var context = new WarehouseDbContext())
            {
                var lastThreePasswords = context.PasswordHistories
                    .Where(p => p.UserId == _user.Id)
                    .OrderByDescending(p => p.ChangeDate)
                    .Take(3)
                    .Select(p => p.Password)
                    .ToList();

                if (lastThreePasswords.Contains(newPassword))
                {
                    MessageBox.Show("Nowe hasło nie może być takie samo jak 3 ostatnie hasła użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!ValidatePassword(newPassword))
                {
                    return; 
                }

                var result = MessageBox.Show("Czy na pewno chcesz zapisać zmiany?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var existingUser = context.Users.FirstOrDefault(u => u.Id == _user.Id);
                    if (existingUser != null)
                    {
                        existingUser.Password = newPassword;

                        var historyEntry = new PasswordHistory
                        {
                            UserId = _user.Id,
                            Password = newPassword,
                            ChangeDate = DateTime.Now
                        };

                        context.PasswordHistories.Add(historyEntry);
                        context.SaveChanges();

                        MessageBox.Show("Hasło zostało zmienione.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                }
            }
        }

        private bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                MessageBox.Show("Hasło powinno mieć co najmniej 8 znaków.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                MessageBox.Show("Hasło powinno zawierać co najmniej jedną małą literę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Hasło powinno zawierać co najmniej jedną wielką literę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Hasło powinno zawierać co najmniej jedną cyfrę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string specialChars = "-_!*#$&";
            if (!password.Any(c => specialChars.Contains(c)))
            {
                MessageBox.Show("Hasło powinno zawierać co najmniej jeden ze znaków specjalnych: -, _, !, *, #, $, &", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno chcesz anulować zmiany?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
