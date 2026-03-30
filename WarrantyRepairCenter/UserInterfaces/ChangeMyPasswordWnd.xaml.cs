using System.Windows;
using WarrantyRepairCenter.Authentication;
using WarrantyRepairCenter.BusinessLogicLayer;

namespace WarrantyRepairCenter.UserInterfaces
{
    /// <summary>
    /// Interaction logic for ChangeMyPasswordWnd.xaml
    /// </summary>
    public partial class ChangeMyPasswordWnd : Window
    {
        public ChangeMyPasswordWnd()
        {
            InitializeComponent();
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = txtOldPass.Password;
            string newPassword = txtNewPass.Password;
            if (currentPassword == newPassword)
            {
                MessageBox.Show(this, "New password cannot be the same as the current password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show(this, "Please enter both old and new passwords.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNewPass.Focus();
                return;
            } 
            if (newPassword.Length < 8 || newPassword.Length > 20)
            {
                MessageBox.Show(this, "Password must be between 8 and 20 characters long.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNewPass.Focus();
                return;
            }
            if (newPassword.Contains(' '))
            {
                MessageBox.Show(this, "Password cannot contain spaces.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNewPass.Focus();
                return;
            }
            if (!newPassword.Any(char.IsUpper) || !newPassword.Any(char.IsLower) || !newPassword.Any(char.IsDigit) || !newPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                MessageBox.Show(this, "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNewPass.Focus();
                return;
            }
            if (!AuthHelper.VerifyPassword(currentPassword))
            {
                MessageBox.Show(this, "Current password is incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtOldPass.Focus();
                return;
            }
            if (!EmployeeBLL.Instance.ChangePassword(AuthHelper.CurrentEmployee.ID, newPassword, out string errorMessage))
            {
                MessageBox.Show(this, errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(this, "Password changed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            AuthHelper.Logout();
            DialogResult = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
