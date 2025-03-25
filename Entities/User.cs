using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string PESEL { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender{ get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsForgotten { get; set; }
        public DateTime ForgottenDate { get; set; }
        public int? ForgottenById { get; set; }
        public User ForgotenBy { get; set; }
        public UserPermission UserPermission { get; set; }
        public List<PasswordHistory> PasswordHistories { get; set; }
        public List<User> ForgottenUsers { get; set; }
        //public int UserPermissionId { get; set; }

        //public List<Product> Products { get; set; }
    }
}
