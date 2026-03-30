using System.Windows;
using System.Windows.Controls;
using WarrantyRepairCenter.Authentication;
using WarrantyRepairCenter.BusinessLogicLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.UserInterfaces
{
    /// <summary>
    /// Interaction logic for CustomerWnd.xaml
    /// </summary>
    public partial class CustomerWnd : Window
    {
        public CustomerWnd()
        {
            InitializeComponent();
            UpdateDG();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = txtPhone.Text = txtEmail.Text = txtAddress.Text = string.Empty;
            dgData.SelectedItem = null;
            UpdateDG();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            try
            {
                if (!CustomerBLL.Instance.AddCustomer(name, email, phone, address, out string message))
                {
                    MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
            MessageBox.Show(this, "Customer added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateDG();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Customer? customer = dgData.SelectedItem as Customer;
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            try
            {
                if (!CustomerBLL.Instance.UpdateCustomer(customer?.ID, name, email, phone, address, out string message))
                {
                    MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
            MessageBox.Show(this, "Customer updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateDG();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to remove the selected customer?", "Confirm Removal", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;
            Customer? customer = dgData.SelectedItem as Customer;
            try
            {
                if (!CustomerBLL.Instance.RemoveCustomer(customer?.ID, out string message))
                {
                    MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
            MessageBox.Show(this, "Customer removed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateDG();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is not Customer selectedCustomer)
                return;
            txtName.Text = selectedCustomer.Name;
            txtEmail.Text = selectedCustomer.Email;
            txtPhone.Text = selectedCustomer.PhoneNumber;
            txtAddress.Text = selectedCustomer.Address;
        }

        void UpdateDG()
        {
            try
            {
                dgData.ItemsSource = CustomerBLL.Instance.GetAllCustomers();
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
        }
    }
}
