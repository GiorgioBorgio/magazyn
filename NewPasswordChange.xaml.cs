using System;
using System.Linq;
using System.Windows;
using Magazyn.Entities;

namespace Magazyn
{
    public partial class NewPasswordChange : Window
    {
        private User _user;

        internal NewPasswordChange(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordBox.Password.Trim();
            string confirmPassword = ConfirmPasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Hasła nie są identyczne.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidatePassword(newPassword))
            {
                MessageBox.Show("Hasło musi mieć minimum 8 znaków i zawierać wielką literę, małą literę, cyfrę i znak specjalny.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new WarehouseDbContext())
            {
                var recentPasswords = context.PasswordHistories
                    .Where(p => p.UserId == _user.Id)
                    .OrderByDescending(p => p.ChangeDate)
                    .Take(3)
                    .Select(p => p.Password)
                    .ToList();

                if (recentPasswords.Contains(newPassword))
                {
                    MessageBox.Show("Nowe hasło nie może być takie samo jak jedno z trzech ostatnich.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var userInDb = context.Users.First(u => u.Id == _user.Id);
                userInDb.Password = newPassword;
                //userInDb.MustChangePassword = false;

                context.PasswordHistories.Add(new PasswordHistory
                {
                    UserId = _user.Id,
                    Password = newPassword,
                    ChangeDate = DateTime.Now
                });

                context.SaveChanges();

                MessageBox.Show("Hasło zostało zmienione.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                
                this.Close();
            }
        }

        private bool ValidatePassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => "!@#$%^&*".Contains(ch));
        }
    }
}
