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

namespace WarrantyRepairCenter.UserInterfaces
{
    /// <summary>
    /// Interaction logic for DashboardWnd.xaml
    /// TODO: Show different elements based on user role here
    /// </summary>
    public partial class DashboardWnd : Window
    {
        public DashboardWnd()
        {
            InitializeComponent();
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow wnd = new LoginWindow();
            wnd.Show();
            Close();
        }
    }
}
