using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Magazyn.Entities;
using Magazyn.Models;
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
    public partial class AddModifyUser : Window
    {
        private readonly IMapper _mapper;
        private CreateUserDto _edytowany_user;
        private WarehouseDbContext _context;
        private User _existingUser;
        public AddModifyUser()
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
        internal AddModifyUser(User edytowany_user)
        {
            _context = new WarehouseDbContext();
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

        private bool Walidacja()
        {
            // Poprawione wymagane pola (Ulica jest opcjonalna)
            var wymagane_pola = new Dictionary<string, string>
    {
        {"Login", Textbox_login.Text },
        {"Imię", Textbox_imie.Text },
        {"Nazwisko", Textbox_nazwisko.Text },
        {"Miejscowość", Textbox_miejscowość.Text },
        {"Kod pocztowy", Textbox_kod_pocztowy.Text },
        {"Numer posesji", Textbox_numer_posesji.Text },
        {"Pesel", Textbox_pesel.Text },
        {"Numer telefonu", Textbox_numer_telefonu.Text },
        {"Adres e-mail", Textbox_mail.Text }
    };

            // Walidacja wymaganych pól
            foreach (var pole in wymagane_pola)
            {
                if (string.IsNullOrWhiteSpace(pole.Value))
                {
                    MessageBox.Show($"Proszę wypełnić pole: {pole.Key}.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            // Data urodzenia
            if (Date_picker_data_urodzenia.SelectedDate == null)
            {
                MessageBox.Show("Proszę wybrać datę urodzenia.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Płeć
            if (Radio_btn_kobieta.IsChecked == false && Radio_btn_mężczyzna.IsChecked == false)
            {
                MessageBox.Show("Proszę wybrać płeć.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Walidacja numeru telefonu (9 cyfr)
            if (Textbox_numer_telefonu.Text.Length != 9 || !Textbox_numer_telefonu.Text.All(char.IsDigit))
            {
                MessageBox.Show("Numer telefonu musi składać się z 9 cyfr.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Walidacja kodu pocztowego (format dd-ddd)
            if (!System.Text.RegularExpressions.Regex.IsMatch(Textbox_kod_pocztowy.Text, @"^\d{2}-\d{3}$"))
            {
                MessageBox.Show("Kod pocztowy musi być w formacie XX-XXX.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Walidacja adresu e-mail
            if (Textbox_mail.Text.Length > 255)
            {
                MessageBox.Show("Adres e-mail może mieć maksymalnie 255 znaków.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(Textbox_mail.Text))
            {
                MessageBox.Show("Nieprawidłowy format adresu e-mail.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Walidacja PESEL
            string pesel = Textbox_pesel.Text;
            if (pesel.Length != 11 || !pesel.All(char.IsDigit))
            {
                MessageBox.Show("PESEL musi składać się z 11 cyfr.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            try
            {
                // Sprawdzenie daty urodzenia w PESEL
                int year = int.Parse(pesel.Substring(0, 2));
                int month = int.Parse(pesel.Substring(2, 2));
                int day = int.Parse(pesel.Substring(4, 2));

                int century;
                if (month > 80) { century = 1800; month -= 80; }
                else if (month > 60) { century = 2200; month -= 60; }
                else if (month > 40) { century = 2100; month -= 40; }
                else if (month > 20) { century = 2000; month -= 20; }
                else { century = 1900; }

                DateTime peselDate = new DateTime(century + year, month, day);
                if (peselDate != Date_picker_data_urodzenia.SelectedDate.Value.Date)
                {
                    MessageBox.Show("Data urodzenia nie zgadza się z PESEL.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // Sprawdzenie płci
                int genderDigit = int.Parse(pesel[9].ToString());
                bool isMale = Radio_btn_mężczyzna.IsChecked == true;
                if ((isMale && genderDigit % 2 == 0) || (!isMale && genderDigit % 2 != 0))
                {
                    MessageBox.Show("Płeć nie zgadza się z PESEL.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // Sprawdzenie sumy kontrolnej
                int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
                int sum = 0;
                for (int i = 0; i < 10; i++)
                    sum += int.Parse(pesel[i].ToString()) * weights[i];
                int checksum = (10 - (sum % 10)) % 10;
                if (checksum != int.Parse(pesel[10].ToString()))
                {
                    MessageBox.Show("Nieprawidłowa suma kontrolna PESEL.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Nieprawidłowy format PESEL.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Sprawdzenie unikalności loginu, PESEL i e-maila
            bool isNewUser = _edytowany_user == null;
            int currentUserId = isNewUser ? -1 : _existingUser.Id;

            if (_context.Users.Any(u => u.Login == Textbox_login.Text && u.Id != currentUserId))
            {
                MessageBox.Show("Login jest już zajęty.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_context.Users.Any(u => u.PESEL == pesel && u.Id != currentUserId))
            {
                MessageBox.Show("PESEL jest już w systemie.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_context.Users.Any(u => u.Email == Textbox_mail.Text && u.Id != currentUserId))
            {
                MessageBox.Show("Adres e-mail jest już w użyciu.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        private void Button_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (!Walidacja()) return;
            bool plec;
            var user = new CreateUserDto();
            if (_edytowany_user != null)
            {
                // Aktualizuj dane edytowanego użytkownika
                user.Login = Textbox_login.Text;
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

                //tutaj nie dziala

                if (_existingUser != null)
                {
                    var existingId = _existingUser.Id; // Zapisujemy oryginalne ID
                    _mapper.Map(user, _existingUser);
                    _existingUser.Id = existingId; // Ustawiamy z powrotem prawidłowe ID




                    //address.Street = newUser.Address.Street;
                    //address.City = newUser.Address.City;
                    //address.ApartmentNumber = newUser.Address.ApartmentNumber;
                    //address.HouseNumber = newUser.Address.HouseNumber; 
                    //address.PostalCode = newUser.Address.PostalCode;   
                    //czarny zrub

                }
            }
            else
            {

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
                user.DateOfBirth = (DateTime)Date_picker_data_urodzenia.SelectedDate;
                user.Gender = plec;
                user.Email = Textbox_mail.Text;
                user.PhoneNumber = Textbox_numer_telefonu.Text;
                //mapowanie z dto do user
                //

                User newUser = _mapper.Map<User>(user);
                //MessageBox.Show(_edytowany_user.FirstName);
                _context.Users.Add(newUser);
                _context.Addresses.Add(newUser.Address);

            }
                _context.SaveChanges();




                this.Close();
            }
        }
    }

