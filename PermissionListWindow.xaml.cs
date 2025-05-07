using System.Collections.Generic;
using System.Windows;

namespace Magazyn
{
    public partial class PermissionListWindow : Window
    {
        public PermissionListWindow(List<string> permissions)
        {
            InitializeComponent();
            PermissionList.ItemsSource = permissions;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
