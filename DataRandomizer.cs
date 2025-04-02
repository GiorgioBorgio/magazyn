using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn
{
    internal class DataRandomizer
    {
        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateValidPesel(DateTime birthDate)
        {
            var random = new Random();
            int year = birthDate.Year % 100;
            int month = birthDate.Month;
            int day = birthDate.Day;
            int genderDigit = random.Next(0, 9);

            if (birthDate.Year >= 2000) month += 20;
            else if (birthDate.Year >= 2100) month += 40;
            else if (birthDate.Year >= 2200) month += 60;
            else if (birthDate.Year >= 1800) month += 80;

            string pesel = $"{year:D2}{month:D2}{day:D2}{random.Next(100, 999)}{genderDigit}";
            return pesel + CalculatePeselChecksum(pesel);
        }

        public int CalculatePeselChecksum(string pesel)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(pesel[i].ToString()) * weights[i];
            int checksum = (10 - (sum % 10)) % 10;
            return checksum;
        }

        public DateTime GenerateRandomDateOfBirth()
        {
            var random = new Random();
            int year = random.Next(1950, 2010);
            int month = random.Next(1, 12);
            int day = random.Next(1, DateTime.DaysInMonth(year, month));
            return new DateTime(year, month, day);
        }

        public bool GenerateRandomGender()
        {
            return new Random().Next(2) == 0;
        }

        public string GenerateRandomEmail()
        {
            return $"{GenerateRandomString(8)}@example.com";
        }

        public string GenerateRandomPhoneNumber()
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, 9).Select(_ => random.Next(0, 10).ToString()));
        }
    }
}
