using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Magazyn.Entities;
using Magazyn.Models;
using Magazyn.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddModifyUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private readonly IMapper _mapper;
        private CreateUserDto _edytowany_user;
        private WarehouseDbContext _context;
        private User _existingUser;
        public AddUser()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WarehouseMappingProfile());
            });
            var mapper = config.CreateMapper();
            _mapper = config.CreateMapper();
            _context = new WarehouseDbContext();
            InitializeComponent();

        }
        
        

        
        private void Button_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            UserValidator validator = new UserValidator(_context);
            
            bool plec;
            var user = new CreateUserDto();
            //if (_edytowany_user != null)
            //{
            //    // Aktualizuj dane edytowanego użytkownika
            //    user.Login = Textbox_login.Text;
            //    user.FirstName = Textbox_imie.Text;
            //    user.LastName = Textbox_nazwisko.Text;
            //    user.City = Textbox_miejscowość.Text;
            //    user.PostalCode = Textbox_kod_pocztowy.Text;
            //    user.Street = Textbox_ulica.Text;
            //    user.ApartmentNumber = Textbox_numer_lokalu.Text;
            //    user.HouseNumber = Textbox_numer_posesji.Text;
            //    user.PESEL = Textbox_pesel.Text;
            //    user.DateOfBirth = (DateTime)Date_picker_data_urodzenia.SelectedDate;
            //    user.Email = Textbox_mail.Text;
            //    user.PhoneNumber = Textbox_numer_telefonu.Text;

            //    if (!validator.Walidacja(_edytowany_user)) return;

            //    if (_existingUser != null)
            //    {
            //        var existingId = _existingUser.Id; // Zapisujemy oryginalne ID
            //        user.Gender = Radio_btn_mężczyzna.IsChecked == true; //naprawione mapowanie płci
            //        _mapper.Map(user, _existingUser);
            //        _existingUser.Id = existingId; // Ustawiamy z powrotem prawidłowe ID
  
            //    }
            //}
            

                if (Radio_btn_kobieta.IsChecked == true)
                {
                    plec = false;
                }
                else
                {
                    plec = true;
                }
                user.Login = Textbox_login.Text;
                user.FirstName = Textbox_imie.Text;
                user.LastName = Textbox_nazwisko.Text;
                user.City = Textbox_miejscowość.Text;
                user.PostalCode = Textbox_kod_pocztowy.Text;
                user.Street = Textbox_ulica.Text;
                user.ApartmentNumber = Textbox_numer_lokalu.Text;
                user.HouseNumber = Textbox_numer_posesji.Text;
                user.PESEL = Textbox_pesel.Text;
                if (Date_picker_data_urodzenia.SelectedDate == null) { MessageBox.Show("Proszę wybrać datę urodzenia.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
                user.DateOfBirth = (DateTime)Date_picker_data_urodzenia.SelectedDate;
                user.Gender = plec;
                user.Email = Textbox_mail.Text;
                user.PhoneNumber = Textbox_numer_telefonu.Text;
            //mapowanie z dto do user
            //
                if (!validator.Walidacja(user)) return;
                 User newUser = _mapper.Map<User>(user);
                _context.Users.Add(newUser);
                _context.Addresses.Add(newUser.Address);

            
            _context.SaveChanges();
            //RefreshUserDataGrid();





            this.Close();
            }
        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close(); // Zamknij okno bez zapisu
        }

    }

}

