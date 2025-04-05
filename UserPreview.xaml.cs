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
        WarehouseDbContext _context;
        private User _user;
        UsersManager _usersManager;

        public UserPreview()
        {
        }

        internal UserPreview(User user, UsersManager usersManager)
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
            _user = user;
            _usersManager = usersManager;
            var address = _context.Addresses.Where(e => e.Id == user.AddressId).FirstOrDefault();
            label_firstname.Content = user.FirstName;
            label_lastname.Content = user.LastName;
            label_login.Content = user.Login;
            label_city.Content = address.City;
            label_postalcode.Content = address.PostalCode;
            label_street.Content = address.Street;
            label_streetnumber.Content = address.HouseNumber;
            label_housenumber.Content = address.HouseNumber;
            label_dateofbirth.Content = user.DateOfBirth.ToString();
            label_email.Content = user.Email;
            label_pesel.Content = user.PESEL.ToString();
            if (user.Gender == true)
                label_gender.Content = "Mężczyzna";
            else
                label_gender.Content = "Kobieta";
            label_phonenumber.Content = user.PhoneNumber.ToString();
        }

        private void ButtonEdytujClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            var editWindow = new ModifyUser(_user, _usersManager);
            editWindow.ShowDialog();
            //RefreshUserDataGrid();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}