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
using WarrantyRepairCenter.BusinessLogicLayer;
using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter
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
            bool success = CustomerBLL.Instance.AddCustomer(name, email, phone, address, out string message);
            if (!success)
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            bool success = CustomerBLL.Instance.UpdateCustomer(customer?.ID, name, email, phone, address, out string message);
            if (!success)
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            UpdateDG();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Customer? customer = dgData.SelectedItem as Customer;
            bool success = CustomerBLL.Instance.RemoveCustomer(customer?.ID, out string message);
            if (!success)
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            dgData.ItemsSource = CustomerBLL.Instance.GetAllCustomers();
        }
    }
}
