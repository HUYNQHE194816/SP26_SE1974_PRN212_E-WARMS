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
        }

        private void addCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerDAL.Instance.AddCustomer(new Customer
            {
            });
        }
    }
}
