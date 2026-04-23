using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WarrantyRepairCenter.Authentication;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.UserInterfaces
{
    /// <summary>
    /// Interaction logic for DashboardWnd.xaml
    /// </summary>
    public partial class DashboardWnd : Window
    {
        public DashboardWnd()
        {
            InitializeComponent();
            ContextMenu userCtxMenu = new ContextMenu();
            MenuItem changePasswordMenuItem = new MenuItem() { Header = "Change password" };
            changePasswordMenuItem.Click += changePasswordMenuItem_Click;
            userCtxMenu.Items.Add(changePasswordMenuItem);
            MenuItem logoutMenuItem = new MenuItem() { Header = "Logout" };
            logoutMenuItem.Click += logoutMenuItem_Click;
            userCtxMenu.Items.Add(logoutMenuItem);
            btnUserMenu.ContextMenu = userCtxMenu;
            lbDisplayName.Text = AuthHelper.CurrentEmployee.FullName;
            lbRole.Text = AuthHelper.CurrentEmployee.Role.ToString();
            if (AuthHelper.CurrentEmployee.Role != EmployeeRole.Admin && AuthHelper.CurrentEmployee.Role != EmployeeRole.Manager)
                btnEmployees.Visibility = Visibility.Collapsed;
        }

        private void changePasswordMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            ChangeMyPasswordWnd wnd = new ChangeMyPasswordWnd();
            if (wnd.ShowDialog() != true)
            {
                Show();
                return;
            }
            LoginWindow loginWnd = new LoginWindow();
            loginWnd.Show();
            Close();
        }

        private void logoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to logout?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;
            AuthHelper.Logout();
            LoginWindow wnd = new LoginWindow();
            wnd.Show();
            Close();
        }

        private void btnUserMenu_Click(object sender, RoutedEventArgs e)
        {
            btnUserMenu.ContextMenu.Placement = PlacementMode.MousePoint;
            btnUserMenu.ContextMenu.IsOpen = true;
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomerWnd wnd = new CustomerWnd();
            Hide();
            wnd.ShowDialog();
            Show();
        }

        private void btnRepairTickets_Click(object sender, RoutedEventArgs e)
        {
            RepairTicketWnd wnd = new RepairTicketWnd();
            Hide();
            wnd.ShowDialog();
            Show();
        }

        private void btnParts_Click(object sender, RoutedEventArgs e)
        {
            SparePartWnd wnd = new SparePartWnd();
            Hide();
            wnd.ShowDialog();
            Show();
        }

        private void btnDevices_Click(object sender, RoutedEventArgs e)
        {
            DeviceWnd wnd = new DeviceWnd();
            Hide();
            wnd.ShowDialog();
            Show();
        }

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            ServiceWnd wnd = new ServiceWnd();
            Hide();
            wnd.ShowDialog();
            Show();
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (AuthHelper.CurrentEmployee.Role != EmployeeRole.Admin)
            {
                MessageBox.Show(this, "You do not have permission to access this feature.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            EmployeeWnd wnd = new EmployeeWnd();
            Hide();
            wnd.ShowDialog();
            Show();
        }
    }
}
