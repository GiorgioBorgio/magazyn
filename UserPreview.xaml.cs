using Magazyn.Entities;
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
    /// Logika interakcji dla klasy Podglad.xaml
    /// </summary>
    public partial class UserPreview : Window
    {
        internal UserPreview(User user)
        {
            InitializeComponent();
            label_imie.Content = user.FirstName;
            label_nazwisko.Content = user.LastName;
            label_login.Content = user.Login;
            label_adres.Content = user.Address;
            label_data_urodzenia.Content = user.DateOfBirth.ToString();
            label_mail.Content = user.Email;
            label_pesel.Content = user.PESEL.ToString();
            label_plec.Content = user.Gender;
            label_nr_tel.Content = user.PhoneNumber.ToString();
        }
    }
}
