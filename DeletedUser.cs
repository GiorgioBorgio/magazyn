using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn
{
    public class DeletedUser
    {
        static List<DeletedUsers> deletedUsers = new List<DeletedUsers>();
        int id;
        string name;
        string surname;
        DateTime data_zapomnienia;
        int id_usera_ktory_zapomnial;

        public static List<DeletedUsers> DeletedUsers_ls { get => deletedUsers; set => deletedUsers = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime Data_zapomnienia { get => data_zapomnienia; set => data_zapomnienia = value; }
        public int Id_usera_ktory_zapomnial { get => id_usera_ktory_zapomnial; set => id_usera_ktory_zapomnial = value; }
    }
}
