using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn
{
    internal class DataRandomizer
    {
        private readonly Random random = new Random();
        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateValidPesel(DateTime birthDate, bool gender)
        {
            int year = birthDate.Year % 100;
            int month = birthDate.Month;
            int day = birthDate.Day;

            // Określanie serii miesiąca w zależności od wieku
            if (birthDate.Year >= 2000 && birthDate.Year < 2100) month += 20;
            else if (birthDate.Year >= 2100 && birthDate.Year < 2200) month += 40;
            else if (birthDate.Year >= 2200 && birthDate.Year < 2300) month += 60;
            else if (birthDate.Year >= 1800 && birthDate.Year < 1900) month += 80;

            // Generowanie losowej serii 3 cyfr
            int serialNumber = random.Next(100, 1000); // przy next górna granica jest wyłączna, więc 1000 nie zostanie wygenerowane

            // Generowanie cyfry kontrolnej płci (parzysta dla kobiet, nieparzysta dla mężczyzn)
            int genderDigit = gender ? random.Next(0, 5) * 2 + 1 : random.Next(0, 5) * 2;

            // Składanie numeru PESEL
            string peselBase = $"{year:D2}{month:D2}{day:D2}{serialNumber}{genderDigit}";
            int checksum = CalculatePeselChecksum(peselBase);

            return peselBase + checksum;
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
            int year = random.Next(1950, 2011); // jw
            int month = random.Next(1, 13); // jw
            int day = random.Next(1, DateTime.DaysInMonth(year, month));
            return new DateTime(year, month, day);
        }

        public bool GenerateRandomGender()
        {
            return random.Next(2) == 0;
        }

        public string GenerateRandomEmail()
        {
            return $"{GenerateRandomString(8)}@example.com";
        }

        public string GenerateRandomPhoneNumber()
        {
            return string.Concat(Enumerable.Range(0, 9).Select(_ => random.Next(0, 10).ToString()));
        }
    }
}
