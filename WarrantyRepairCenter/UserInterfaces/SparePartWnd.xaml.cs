using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WarrantyRepairCenter.Authentication;
using WarrantyRepairCenter.BusinessLogicLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.UserInterfaces
{
    /// <summary>
    /// Interaction logic for SparePartWnd.xaml
    /// </summary>
    public partial class SparePartWnd : Window
    {
        public SparePartWnd()
        {
            InitializeComponent();
            UpdateDG();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = txtSKU.Text = txtImportPrice.Text = txtSellingPrice.Text = txtStockQuantity.Text = txtWarrantyPeriodMonth.Text = string.Empty;
            dgData.SelectedItem = null;
            UpdateDG();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string sku = txtSKU.Text.Trim();
            if (!decimal.TryParse(txtImportPrice.Text.Trim(), out decimal importPrice))
            {
                MessageBox.Show(this, "Invalid import price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!decimal.TryParse(txtSellingPrice.Text.Trim(), out decimal sellingPrice))
            {
                MessageBox.Show(this, "Invalid selling price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(txtStockQuantity.Text.Trim(), out int stockQuantity))
            {
                MessageBox.Show(this, "Invalid stock quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(txtWarrantyPeriodMonth.Text.Trim(), out int warrantyPeriodMonth))
            {
                MessageBox.Show(this, "Invalid warranty period month.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (!SparePartBLL.Instance.AddSparePart(name, sku, importPrice, sellingPrice, stockQuantity, warrantyPeriodMonth, out string message))
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
            SparePart? sparePart = dgData.SelectedItem as SparePart;
            string name = txtName.Text.Trim();
            string sku = txtSKU.Text.Trim();
            if (!decimal.TryParse(txtImportPrice.Text.Trim(), out decimal importPrice))
            {
                MessageBox.Show(this, "Invalid import price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!decimal.TryParse(txtSellingPrice.Text.Trim(), out decimal sellingPrice))
            {
                MessageBox.Show(this, "Invalid selling price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(txtStockQuantity.Text.Trim(), out int stockQuantity))
            {
                MessageBox.Show(this, "Invalid stock quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(txtWarrantyPeriodMonth.Text.Trim(), out int warrantyPeriodMonth))
            {
                MessageBox.Show(this, "Invalid warranty period month.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (!SparePartBLL.Instance.UpdateSparePart(sparePart?.ID, name, sku, importPrice, sellingPrice, stockQuantity, warrantyPeriodMonth, out string message))
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
            if (MessageBox.Show(this, "Are you sure you want to remove the selected spare part?", "Confirm Removal", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;
            SparePart? sparePart = dgData.SelectedItem as SparePart;
            try
            {
                if (!SparePartBLL.Instance.RemoveSparePart(sparePart?.ID, out string message))
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
            if (dgData.SelectedItem is not SparePart sparePart)
                return;

            txtName.Text = sparePart.Name;
            txtSKU.Text = sparePart.SKU;
            txtImportPrice.Text = sparePart.ImportPrice.ToString("F2");
            txtSellingPrice.Text = sparePart.SellingPrice.ToString("F2");
            txtStockQuantity.Text = sparePart.StockQuantity.ToString();
            txtWarrantyPeriodMonth.Text = sparePart.WarrantyPeriodMonth.ToString();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string GenerateSKUFromName(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                    return new string('X', 10);
                string normalized = name.Normalize(NormalizationForm.FormD).Replace('ð', 'd').Replace('Ð', 'D');
                StringBuilder sb = new StringBuilder();
                foreach (char c in normalized)
                {
                    UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (category == UnicodeCategory.NonSpacingMark)
                        continue;
                    if (char.IsLetterOrDigit(c))
                        sb.Append(char.ToUpperInvariant(c));
                    else if (char.IsWhiteSpace(c))
                        sb.Append(' ');
                }
                string cleaned = sb.ToString().Trim();
                if (cleaned.Length == 0)
                    return new string('X', 10);
                string[] words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                StringBuilder prefixBuilder = new StringBuilder();
                foreach (string word in words)
                {
                    if (word.Length == 0)
                        continue;
                    prefixBuilder.Append(word[0]);
                    if (prefixBuilder.Length == 5)
                        break;
                }
                string prefix = prefixBuilder.Length == 0 ? "X" : prefixBuilder.ToString();
                byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(cleaned));
                string suffix = Convert.ToHexString(hash)[..(10 - prefix.Length)];
                return prefix + suffix;
            }

            txtSKU.Text = GenerateSKUFromName(txtName.Text.Trim());
        }

        void UpdateDG()
        {
            try
            {
                dgData.ItemsSource = SparePartBLL.Instance.GetAllSpareParts();
            }
            catch (UnauthenticatedException)
            {
                Close();
                return;
            }
        }
    }
}
