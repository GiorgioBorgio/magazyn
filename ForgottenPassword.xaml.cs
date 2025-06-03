using System;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using Magazyn.Entities;

namespace Magazyn
{
    public partial class ForgottenPassword : Window
    {
        public ForgottenPassword()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextbox.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Proszę wpisać adres e-mail.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new WarehouseDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    MessageBox.Show("Użytkownik z takim adresem e-mail nie istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    SendResetEmail(user, context);
                    MessageBox.Show("Wysłano e-mail z nowym hasłem.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd przy wysyłaniu wiadomości: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SendResetEmail(User user, WarehouseDbContext context)
        {
            string newPassword = GenerateRandomPassword();

        
            user.Password = newPassword;

            //user.MustChangePassword = true;

            context.PasswordHistories.Add(new PasswordHistory
            {
                UserId = user.Id,
                Password = newPassword,
                ChangeDate = DateTime.Now
            });

            context.SaveChanges();

            
            var fromAddress = new MailAddress(EmailTextbox.Text, "Magazyn - reset hasła");
            var toAddress = new MailAddress(user.Email);
            const string fromPassword = ""; 
            const string subject = "Nowe hasło";

            string body = $"Witaj {user.FirstName},\n\nTwoje nowe hasło to: {newPassword}\nZaloguj się i zmień hasło jak najszybciej.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        private string GenerateRandomPassword()
        {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*";

            var random = new Random();
            var passwordChars = new List<char>();

            // Dodaj wymaganą liczbę znaków z każdej kategorii
            passwordChars.AddRange(Enumerable.Range(0, 3).Select(_ => uppercase[random.Next(uppercase.Length)]));
            passwordChars.AddRange(Enumerable.Range(0, 3).Select(_ => lowercase[random.Next(lowercase.Length)]));
            passwordChars.AddRange(Enumerable.Range(0, 2).Select(_ => digits[random.Next(digits.Length)]));
            passwordChars.AddRange(Enumerable.Range(0, 2).Select(_ => special[random.Next(special.Length)]));

            // Tasowanie, aby znaki były w losowej kolejności
            passwordChars = passwordChars.OrderBy(_ => random.Next()).ToList();

            return new string(passwordChars.ToArray());
        }


        

    }
}
