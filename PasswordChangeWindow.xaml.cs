using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Magazyn.Entities;

namespace Magazyn
{
    public partial class PasswordChangeWindow : Window
    {
        private User _user;
        private List<string> _passwordHistory; // ostatnie 3 hasła

        internal PasswordChangeWindow(User user, List<string> passwordHistory)
        {
            InitializeComponent();
            _user = user;
            _passwordHistory = passwordHistory;
            LoginTextBox.Text = _user.Login;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordBox.Password;

            if (_passwordHistory.Contains(newPassword))
            {
                MessageBox.Show("Nowe hasło nie może być takie samo jak 3 ostatnie hasła użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ValidatePassword(newPassword))
            {
                MessageBox.Show("Hasło musi zawierać co najmniej 8 znaków, w tym cyfrę i znak specjalny.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Czy na pewno chcesz zapisać zmiany?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SaveNewPassword(_user, newPassword);
                MessageBox.Show("Hasło zostało zmienione.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private bool ValidatePassword(string password)
        {
            return password.Length >= 8 && password.Any(char.IsDigit) && password.Any(char.IsPunctuation);
        }

        private void SaveNewPassword(User user, string password)
        {
            // TODO: zapis hasła do bazy danych i aktualizacja historii haseł
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}