using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Entities
{
    internal class PasswordHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime ChangeDate { get; set; }
        public User User { get; set; }
    }
}
