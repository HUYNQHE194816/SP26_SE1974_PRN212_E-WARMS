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
using WarrantyRepairCenter.Authentication;
using WarrantyRepairCenter.BusinessLogicLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.UserInterfaces
{
    /// <summary>
    /// Interaction logic for ServiceWnd.xaml
    /// </summary>
    public partial class ServiceWnd : Window
    {
        public ServiceWnd()
        {
            InitializeComponent();
            UpdateDG();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = txtDesc.Text = txtBasePrice.Text = string.Empty;
            dgData.SelectedItem = null;
            UpdateDG();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string desc = txtDesc.Text.Trim();
            if (!decimal.TryParse(txtBasePrice.Text.Trim(), out decimal basePrice))
            {
                MessageBox.Show(this, "Invalid base price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (!ServiceItemBLL.Instance.AddService(name, desc, basePrice, out string message))
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
            UpdateDG();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ServiceItem? serviceItem = dgData.SelectedItem as ServiceItem;
            string name = txtName.Text.Trim();
            string desc = txtDesc.Text.Trim();
            if (!decimal.TryParse(txtBasePrice.Text.Trim(), out decimal basePrice))
            {
                MessageBox.Show(this, "Invalid base price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (!ServiceItemBLL.Instance.UpdateService(serviceItem?.ID, name, desc, basePrice, out string message))
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
            UpdateDG();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to remove the selected service?", "Confirm Removal", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;
            ServiceItem? serviceItem = dgData.SelectedItem as ServiceItem;
            try
            {
                if (!ServiceItemBLL.Instance.RemoveService(serviceItem?.ID, out string message))
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
            UpdateDG();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is not ServiceItem serviceItem)
                return;
            txtName.Text = serviceItem.Name;
            txtDesc.Text = serviceItem.Description;
            txtBasePrice.Text = serviceItem.BasePrice.ToString("F2");
        }

        void UpdateDG()
        {
            try
            {
                dgData.ItemsSource = ServiceItemBLL.Instance.GetAllServices();
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
        }
    }
}
