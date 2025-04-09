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
            Date_picker_data_urodzenia.SelectedDate = DateTime.Today;
        }
        
        

        
        private void Button_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            UserValidator validator = new UserValidator(_context);
            
            bool plec;

          
            var user = new CreateUserDto();
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
            user.DateOfBirth = Date_picker_data_urodzenia.SelectedDate;
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
            if (!IsChanged()) { this.Close(); return; }
            MessageBoxResult result = MessageBox.Show("Wszystkie wprowadzone dane zostaną utracone, czy na pewno chcesz kontynuować?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close(); // Zamknij okno bez zapisu
            }
        }

        private void TextBox_mac_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Zezwalaj tylko na cyfry
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void TextboxLetter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Zezwalaj tylko na litery
            e.Handled = !e.Text.All(char.IsLetter);
        }
        private bool IsChanged()
        {
            // Sprawdzenie, czy jakiekolwiek pole zostało wypełnione
            return !string.IsNullOrEmpty(Textbox_login.Text) ||
                   !string.IsNullOrEmpty(Textbox_imie.Text) ||
                   !string.IsNullOrEmpty(Textbox_nazwisko.Text) ||
                   !string.IsNullOrEmpty(Textbox_miejscowość.Text) ||
                   (Textbox_kod_pocztowy.Text != "__-___") ||
                   !string.IsNullOrEmpty(Textbox_ulica.Text) ||
                   !string.IsNullOrEmpty(Textbox_numer_lokalu.Text) ||
                   !string.IsNullOrEmpty(Textbox_numer_posesji.Text) ||
                   !string.IsNullOrEmpty(Textbox_pesel.Text) ||
                   !string.IsNullOrEmpty(Textbox_mail.Text) ||
                   !string.IsNullOrEmpty(Textbox_numer_telefonu.Text);
        }
    }

}

