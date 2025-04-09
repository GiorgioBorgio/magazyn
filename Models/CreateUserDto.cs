using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Models
{
    internal class CreateUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Login { get; set; }
        public string PESEL { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
        public bool IsForgotten { get; set; }
        public DateTime ForgottenDate { get; set; }
        public int? ForgottenById { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }
}
