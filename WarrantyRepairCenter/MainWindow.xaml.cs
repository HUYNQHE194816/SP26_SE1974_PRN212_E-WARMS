using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WarrantyRepairCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void manageCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerWnd wnd = new CustomerWnd();
            wnd.ShowDialog();
        }
        private void manageEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWnd wnd = new EmployeeWnd();
            wnd.ShowDialog();
        }
        private void manageRepairTicketBtn_Click(object sender, RoutedEventArgs e)
        {
            RepairTicketWnd wnd = new RepairTicketWnd();
            wnd.ShowDialog();
        }

    }
}