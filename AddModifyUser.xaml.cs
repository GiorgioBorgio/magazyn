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
                edytowany_user = _context.Users.Include(u=>u.Address).First(e=>e.Id == userId);
                _edytowany_user = _mapper.Map<CreateUserDto>(edytowany_user);

                // Wypełnij formularz istniejącymi danymi użytkownika
                

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
            }
        }

        private bool Walidacja()
        {
            // Tworzenie słownika z wymaganymi polami
            var wymagane_pola = new Dictionary<string, string>
            {
                {"Imię", Textbox_imie.Text },
                {"Nazwisko", Textbox_nazwisko.Text },
                {"Miejscowość", Textbox_miejscowość.Text },
                {"Kod pocztowy", Textbox_kod_pocztowy.Text },
                {"Ulica", Textbox_ulica.Text },
                {"Numer posesji", Textbox_numer_posesji.Text },
                {"Pesel", Textbox_pesel.Text },
                {"Numer telefonu", Textbox_numer_telefonu.Text },
                {"Adres e-mail", Textbox_mail.Text }
            };

            // Walidacja pól
            foreach (var pole in wymagane_pola)
            {
                if (string.IsNullOrWhiteSpace(pole.Value))
                {
                    MessageBox.Show($"Proszę wypełnić pole: {pole.Key}.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            // Sprawdzanie dodatkowych elementów (np. DataPicker i płci)
            if (Date_picker_data_urodzenia.SelectedDate == null)
            {
                MessageBox.Show("Proszę wybrać datę urodzenia.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Radio_btn_kobieta.IsChecked == false && Radio_btn_mężczyzna.IsChecked == false)
            {
                MessageBox.Show("Proszę wybrać płeć.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Textbox_pesel.Text.Length != 11 || !(Textbox_pesel.Text.All(char.IsDigit)))
            {
                MessageBox.Show("Proszę podać poprawny pesel.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true; // Wszystkie pola są poprawne
        }
        private void Button_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (!Walidacja()) return;
            bool plec;
            var user = new CreateUserDto();
            if (_edytowany_user != null)
            {
                // Aktualizuj dane edytowanego użytkownika
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
                User newUser = _mapper.Map<User>(user);
                _context.Users.Update(newUser);
                var address = _context.Addresses.First(e=>e.Id == newUser.AddressId);
                address.Street = newUser.Address.Street;
                address.City = newUser.Address.City;
                address.ApartmentNumber = newUser.Address.ApartmentNumber;
                address.HouseNumber = newUser.Address.HouseNumber; 
                address.PostalCode = newUser.Address.PostalCode;   
                //czarny zrub

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
