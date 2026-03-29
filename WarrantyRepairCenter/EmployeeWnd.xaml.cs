using System.Windows;
using System.Windows.Controls;
using WarrantyRepairCenter.BusinessLogicLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter
{
    public partial class EmployeeWnd : Window
    {
        public EmployeeWnd()
        {
            InitializeComponent();
            cboRole.ItemsSource = Enum.GetValues(typeof(Role));
            cboRole.SelectedIndex = 0;
            UpdateDG();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            dgData.SelectedItem = null;
            UpdateDG();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = pwdPassword.Password;
            Role role = (Role)cboRole.SelectedItem;

            bool success = EmployeeBLL.Instance.AddEmployee(fullName, username, password, role, out string message);
            MessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (success) UpdateDG();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Employee? employee = dgData.SelectedItem as Employee;
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            Role role = (Role)cboRole.SelectedItem;

            bool success = EmployeeBLL.Instance.UpdateEmployee(employee?.ID, fullName, username, role, out string message);
            MessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (success) UpdateDG();
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            Employee? employee = dgData.SelectedItem as Employee;
            string newPassword = pwdPassword.Password;

            bool success = EmployeeBLL.Instance.ChangePassword(employee?.ID, newPassword, out string message);
            MessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Employee? employee = dgData.SelectedItem as Employee;
            if (employee is null)
            {
                MessageBox.Show("Please select an employee to remove.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var confirm = MessageBox.Show($"Remove employee '{employee.FullName}'?", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            bool success = EmployeeBLL.Instance.RemoveEmployee(employee.ID, out string message);
            MessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (success) { ClearInputs(); UpdateDG(); }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is not Employee selected) return;
            txtFullName.Text = selected.FullName;
            txtUsername.Text = selected.Username;
            pwdPassword.Clear();
            cboRole.SelectedItem = selected.Role;
        }

        void UpdateDG() => dgData.ItemsSource = EmployeeBLL.Instance.GetAllEmployees();

        void ClearInputs()
        {
            txtFullName.Text = txtUsername.Text = string.Empty;
            pwdPassword.Clear();
            cboRole.SelectedIndex = 0;
        }
    }
}