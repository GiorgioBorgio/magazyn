using Magazyn.Entities;
using Magazyn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Magazyn.Validators
{
    internal class UserValidator
    {
        private readonly WarehouseDbContext _context;

        public UserValidator(WarehouseDbContext context)
        {
            _context = context;
        }
        public bool Walidacja(CreateUserDto dto)
        {
            WalidacjaPol(dto);

            if (_context.Users.Any(u => u.Login == dto.Login))
            {
                MessageBox.Show("Login jest już zajęty.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_context.Users.Any(u => u.PESEL == dto.PESEL))
            {
                MessageBox.Show("PESEL jest już w systemie.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_context.Users.Any(u => u.Email == dto.Email))
            {
                MessageBox.Show("Adres e-mail jest już w użyciu.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        public bool Walidacja(CreateUserDto dto, User _existingUser)
        {
            var DataValidation = WalidacjaPol(dto);
            if (!DataValidation)
                return false;
            // Sprawdzenie unikalności loginu, PESEL i e-maila
            bool isNewUser = dto == null;
            int currentUserId = isNewUser ? -1 : _existingUser.Id;

            if (_context.Users.Any(u => u.Login == dto.Login && u.Id != currentUserId))
            {
                MessageBox.Show("Login jest już zajęty.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_context.Users.Any(u => u.PESEL == dto.PESEL && u.Id != currentUserId))
            {
                MessageBox.Show("PESEL jest już w systemie.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_context.Users.Any(u => u.Email == dto.Email && u.Id != currentUserId))
            {
                MessageBox.Show("Adres e-mail jest już w użyciu.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        private bool WalidacjaPol(CreateUserDto dto)
        {
            var wymagane_pola = new Dictionary<string, string>
             {
                        {"Login", dto.Login },
                        {"Imię", dto.FirstName },
                        {"Nazwisko", dto.LastName },
                        {"Miejscowość", dto.City },
                        {"Kod pocztowy", dto.PostalCode },
                        {"Numer posesji", dto.HouseNumber },
                        {"Pesel", dto.PESEL },
                        {"Adres e-mail", dto.Email },
                        {"Numer telefonu", dto.PhoneNumber },
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
            if (dto.DateOfBirth == null)
            {
                MessageBox.Show("Proszę wybrać datę urodzenia.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Płeć
            if (dto.Gender == null)
            {
                MessageBox.Show("Proszę wybrać płeć.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Walidacja numeru telefonu (9 cyfr)
            if (dto.PhoneNumber.Length != 9 || !dto.PhoneNumber.All(char.IsDigit))
            {
                MessageBox.Show("Numer telefonu musi składać się z 9 cyfr.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Walidacja kodu pocztowego (format dd-ddd)
            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.PostalCode, @"^\d{2}-\d{3}$"))
            {
                MessageBox.Show("Kod pocztowy musi być w formacie XX-XXX.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Walidacja adresu e-mail
            if (dto.Email.Length > 255)
            {
                MessageBox.Show("Adres e-mail może mieć maksymalnie 255 znaków.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^(?![.])(?!.*[.]{2})[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!emailRegex.IsMatch(dto.Email))
            {
                MessageBox.Show("Nieprawidłowy format adresu e-mail.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Walidacja PESEL
            string pesel = dto.PESEL;
            if (pesel.Length != 11 || !pesel.All(char.IsDigit))
            {
                MessageBox.Show("PESEL musi składać się z 11 cyfr.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

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
            if (peselDate != dto.DateOfBirth.Value)
            {
                MessageBox.Show("Niepoprawny PESEL.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Sprawdzenie płci
            int genderDigit = int.Parse(pesel[9].ToString());
            bool isMale = dto.Gender;
            if ((isMale && genderDigit % 2 == 0) || (!isMale && genderDigit % 2 != 0))
            {
                MessageBox.Show("Niepoprawny PESEL.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBox.Show("Niepoprawny PESEL.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
