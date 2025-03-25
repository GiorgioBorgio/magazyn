using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Entities
{
    internal class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }
}
