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
                    MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
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
                    MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
            UpdateDG();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Customer? customer = dgData.SelectedItem as Customer;
            try
            {
                if (!CustomerBLL.Instance.RemoveCustomer(customer?.ID, out string message))
                {
                    MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
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
