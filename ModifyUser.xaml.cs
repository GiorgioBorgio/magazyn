using AutoMapper;
using Magazyn.Entities;
using Magazyn.Models;
using Magazyn.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// Interaction logic for ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : Window
    {
        private readonly IMapper _mapper;
        private CreateUserDto _edytowany_user;
        private WarehouseDbContext _context;
        private User _existingUser;
        private UsersManager _usersManager;
        private UserValidator _userValidator;
        internal ModifyUser(User edytowany_user, UsersManager usersManager)
        {
            _usersManager = usersManager;
            _context = new WarehouseDbContext();
            _userValidator = new UserValidator(_context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WarehouseMappingProfile());
            });
            var mapper = config.CreateMapper();
            _mapper = config.CreateMapper();
            InitializeComponent();
            if (edytowany_user != null)
            {
                int userId = edytowany_user.Id;
                edytowany_user = _context.Users.Include(u => u.Address).First(e => e.Id == userId);
                _edytowany_user = _mapper.Map<CreateUserDto>(edytowany_user);

                // Wypełnij formularz istniejącymi danymi użytkownika

                Textbox_login.Text = _edytowany_user.Login;
                Textbox_imie.Text = _edytowany_user.FirstName;
                Textbox_nazwisko.Text = _edytowany_user.LastName;
                Textbox_miejscowość.Text = _edytowany_user.City;
                Textbox_kod_pocztowy.Text = _edytowany_user.PostalCode;
                Textbox_ulica.Text = _edytowany_user.Street;
                Textbox_numer_lokalu.Text = _edytowany_user.ApartmentNumber;
                Textbox_numer_posesji.Text = _edytowany_user.HouseNumber;
                Textbox_pesel.Text = _edytowany_user.PESEL;
                Date_picker_data_urodzenia.SelectedDate = _edytowany_user.DateOfBirth;
                Textbox_mail.Text = _edytowany_user.Email;
                Textbox_numer_telefonu.Text = _edytowany_user.PhoneNumber;

                if (_edytowany_user.Gender == false)
                {
                    Radio_btn_kobieta.IsChecked = true;
                }
                else
                {
                    Radio_btn_mężczyzna.IsChecked = true;
                }
                _existingUser = edytowany_user;
            }
        }

        private void ButtonEdytujClick(object sender, RoutedEventArgs e)
        {
           
                var user = new CreateUserDto();
                user.Login = Textbox_login.Text;
                if(Radio_btn_kobieta.IsChecked == true)
                {
                    user.Gender = false;
                }
                else
                {
                    user.Gender = true;
                }
                user.FirstName = Textbox_imie.Text;
                user.LastName = Textbox_nazwisko.Text;
                user.City = Textbox_miejscowość.Text;
                user.PostalCode = Textbox_kod_pocztowy.Text;
                user.Street = Textbox_ulica.Text;
                user.ApartmentNumber = Textbox_numer_lokalu.Text;
                user.HouseNumber = Textbox_numer_posesji.Text;
                user.PESEL = Textbox_pesel.Text;
                user.DateOfBirth = (DateTime)Date_picker_data_urodzenia.SelectedDate;
                user.Email = Textbox_mail.Text;
                user.PhoneNumber = Textbox_numer_telefonu.Text;
                if (!_userValidator.Walidacja(user, _existingUser)) return;
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zapisac zmiany?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
            {

                if (_existingUser != null)
                {
                    var existingId = _existingUser.Id; // Zapisujemy oryginalne ID
                    user.Gender = Radio_btn_mężczyzna.IsChecked == true; //naprawione mapowanie płci
                    _mapper.Map(user, _existingUser); 
                    _existingUser.Id = existingId; // Ustawiamy z powrotem prawidłowe ID

                }
                _context.SaveChanges();
                _usersManager.RefreshUserDataGrid();
                this.Close();
            }
            
        }
        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close(); // Zamknij okno bez zapisu
        }
    }
}
