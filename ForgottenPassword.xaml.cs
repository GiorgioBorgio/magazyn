using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy ForgottenPassword.xaml
    /// </summary>
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
                    SendResetEmail(user);
                    MessageBox.Show("Wysłano e-mail z instrukcjami resetowania hasła.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd przy wysyłaniu wiadomości: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SendResetEmail(User user)
        {
            //var fromAddress = new MailAddress("lucas003gg@gmail.com", "Magazyn - reset hasła");
            //var toAddress = new MailAddress(user.Email);
            //const string fromPassword = "twoje_hasło_app"; 
            //const string subject = "Reset hasła";
            //string body = $"Witaj {user.FirstName},\n\nKliknij w poniższy link, aby zresetować hasło:\nhttps://twojastrona/reset/{user.Id}";

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
            //};

            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}
        }


    }
}
