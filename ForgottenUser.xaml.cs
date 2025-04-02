using Magazyn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logika interakcji dla klasy DeletedUsers.xaml
    /// </summary>
    public partial class ForgottenUser : Window
    {
        private WarehouseDbContext _context;
        public ForgottenUser()
        {
            InitializeComponent();
            _context = new WarehouseDbContext();
            ForgottenDataGrid.ItemsSource = _context.Users.Where(e=>e.IsForgotten == true).ToList();
        }
    }
}
