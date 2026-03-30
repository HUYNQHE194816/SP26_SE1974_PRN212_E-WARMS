using System.Windows;
using System.Windows.Controls;
using WarrantyRepairCenter.Authentication;
using WarrantyRepairCenter.BusinessLogicLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.UserInterfaces
{
    /// <summary>
    /// Interaction logic for DeviceWnd.xaml
    /// </summary>
    public partial class DeviceWnd : Window
    {
        public DeviceWnd()
        {
            InitializeComponent();
            LoadCustomers();
            UpdateDG();
        }

        private void LoadCustomers()
        {
            try
            {
                cboCustomer.ItemsSource = DeviceBLL.Instance.GetAllCustomers();
            }
            catch (UnauthenticatedException)
            {
                Close();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            LoadCustomers();
            UpdateDG();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string serial = txtSerial.Text.Trim();
            string type = txtType.Text.Trim();
            string brand = txtBrand.Text.Trim();
            string model = txtModel.Text.Trim();
            string color = txtColor.Text.Trim();
            string condition = txtCondition.Text.Trim();
            string notes = txtNotes.Text.Trim();
            Guid? customerId = cboCustomer.SelectedValue as Guid?;
            try
            {
                if (!DeviceBLL.Instance.AddDevice(name, description, serial, type, brand, model, color, condition, notes, customerId, out string message))
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
            MessageBox.Show(this, "Device added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateDG();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Device? device = dgData.SelectedItem as Device;
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string serial = txtSerial.Text.Trim();
            string type = txtType.Text.Trim();
            string brand = txtBrand.Text.Trim();
            string model = txtModel.Text.Trim();
            string color = txtColor.Text.Trim();
            string condition = txtCondition.Text.Trim();
            string notes = txtNotes.Text.Trim();
            Guid? customerId = cboCustomer.SelectedValue as Guid?;
            try
            {
                if (!DeviceBLL.Instance.UpdateDevice(device?.ID, name, description, serial, type, brand, model, color, condition, notes, customerId, out string message))
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
            MessageBox.Show(this, "Device updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateDG();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to remove the selected device?", "Confirm Removal", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;
            Device? device = dgData.SelectedItem as Device;
            try
            {
                if (!DeviceBLL.Instance.RemoveDevice(device?.ID, out string message))
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
            MessageBox.Show(this, "Device removed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateDG();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is not Device selectedDevice)
                return;
            txtName.Text = selectedDevice.Name;
            txtDescription.Text = selectedDevice.Description;
            txtSerial.Text = selectedDevice.Serial ?? string.Empty;
            txtType.Text = selectedDevice.Type;
            txtBrand.Text = selectedDevice.Brand ?? string.Empty;
            txtModel.Text = selectedDevice.Model ?? string.Empty;
            txtColor.Text = selectedDevice.Color ?? string.Empty;
            txtCondition.Text = selectedDevice.PhysicalCondition ?? string.Empty;
            txtNotes.Text = selectedDevice.Notes ?? string.Empty;
            cboCustomer.SelectedValue = selectedDevice.CustomerID;
        }

        void ClearForm()
        {
            txtName.Text = txtDescription.Text = txtSerial.Text = txtType.Text =
                txtBrand.Text = txtModel.Text = txtColor.Text = txtCondition.Text = txtNotes.Text = string.Empty;
            cboCustomer.SelectedItem = null;
            dgData.SelectedItem = null;
        }

        void UpdateDG()
        {
            try
            {
                dgData.ItemsSource = DeviceBLL.Instance.GetAllDevices();
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
        }
    }
}
