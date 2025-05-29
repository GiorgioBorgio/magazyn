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
using Magazyn.Entities;
using Microsoft.EntityFrameworkCore;


namespace Magazyn
{
    /// <summary>
    /// Logika interakcji dla klasy Login_window.xaml
    /// </summary>
    /// 
    
    public partial class Login_window : Window
    {
        public Login_window()
        {
            InitializeComponent();
        }

        private int failedAttempts = 0;
        private DateTime? lockoutEndTime = null;
        private readonly TimeSpan lockoutDuration = TimeSpan.FromMinutes(10);


        private void Button_logowanie_ok_Click(object sender, RoutedEventArgs e)
        {

           

            if (lockoutEndTime.HasValue && DateTime.Now < lockoutEndTime.Value)
            {
                MessageBox.Show($"Logowanie zablokowane do {lockoutEndTime.Value:HH:mm:ss}.", "Zablokowano", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password;

            using (var context = new Entities.WarehouseDbContext())
            {
                var user = context.Users
                    .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                    .FirstOrDefault(u => u.Login == login);


            


                if (user == null)
                {
                    MessageBox.Show("Podany login nie istnieje.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
               

                if (user.Password != password)
                {
                    failedAttempts++;

                    if (failedAttempts >= 3)
                    {
                        lockoutEndTime = DateTime.Now.Add(lockoutDuration);
                        Button_logowanie_ok.IsEnabled = false;

                        MessageBox.Show("Trzykrotnie podałeś/aś błędne hasło! Spróbuj ponownie za 10 minut.", "Zablokowano", MessageBoxButton.OK, MessageBoxImage.Error);

                        
                        LockoutInfoTextBlock.Text = $"Logowanie dostępne od: {lockoutEndTime.Value:HH:mm:ss}";

                      
                        StartLockoutTimer();
                    }
                    else
                    {
                        MessageBox.Show("Błędne hasło.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return;
                }

                
                failedAttempts = 0;

                MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow(user); 

                mainWindow.Show();
                this.Close();
            }
        }

        private void StartLockoutTimer()
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (s, e) =>
            {
                if (DateTime.Now >= lockoutEndTime)
                {
                    dispatcherTimer.Stop();
                    failedAttempts = 0;
                    lockoutEndTime = null;
                    Button_logowanie_ok.IsEnabled = true;
                    LockoutInfoTextBlock.Text = ""; 
                }
                else
                {
                    LockoutInfoTextBlock.Text = $"Logowanie dostępne od: {lockoutEndTime.Value:HH:mm:ss}";
                }
            };
            dispatcherTimer.Start();
        }

        private void Button_pass_recovery_Click(object sender, RoutedEventArgs e)
        {
            //var passwordChangeWindow = new ForgotPasswordManager();
            //passwordChangeWindow.ShowDialog();
        }
    }
}
