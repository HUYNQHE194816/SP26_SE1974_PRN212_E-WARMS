using System.Windows;
using WarrantyRepairCenter.Authentication;

namespace WarrantyRepairCenter.UserInterfaces
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(this, "Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsername.Focus();
                return;
            }
            if (username.Length < 4 || username.Length > 20)
            {
                MessageBox.Show(this, "Username must be between 4 and 20 characters long.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsername.Focus();
                return;
            }
            if (password.Length < 8 || password.Length > 20)
            {
                MessageBox.Show(this, "Password must be between 8 and 20 characters long.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return;
            }
            if (username.Contains(' ') || password.Contains(' '))
            {
                MessageBox.Show(this, "Username and password cannot contain spaces.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsername.Focus();
                return;
            }
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit) || !password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                MessageBox.Show(this, "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return;
            }
            if (!AuthHelper.Login(username, password, out string failedReason))
            {
                MessageBox.Show(this, failedReason, "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUsername.Focus();
                return;
            }
            DashboardWnd wnd = new DashboardWnd();
            wnd.Show();
            Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}