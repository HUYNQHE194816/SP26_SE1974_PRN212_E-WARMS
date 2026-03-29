using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using WarrantyRepairCenter.BusinessLogicLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter
{
    public partial class RepairTicketWnd : Window
    {
        public RepairTicketWnd()
        {
            InitializeComponent();

            // Populate Status combo
            cboStatus.ItemsSource = Enum.GetValues(typeof(TicketStatus));
            cboStatus.SelectedIndex = 0;

            // Populate Device combo
            cboDevice.ItemsSource = DeviceBLL.Instance.GetAllDevices();

            // Populate Technician combo (show only Technicians)
            var technicians = EmployeeBLL.Instance.GetAllEmployees()
                .Where(e => e.Role == Role.Technician)
                .ToList();
            technicians.Insert(0, null!); // allow unassigned
            cboTechnician.ItemsSource = technicians;
            cboTechnician.SelectedIndex = 0;

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
            Device? device = cboDevice.SelectedItem as Device;
            Employee? technician = cboTechnician.SelectedItem as Employee;

            if (!decimal.TryParse(txtDeposit.Text.Trim(), out decimal deposit))
            {
                MessageBox.Show("Deposit must be a valid number.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool success = RepairTicketBLL.Instance.AddRepairTicket(
                device?.ID ?? Guid.Empty,
                txtCondition.Text.Trim(),
                txtNotes.Text.Trim(),
                dpAppointment.SelectedDate,
                deposit,
                technician?.ID,
                out string message);

            MessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (success) UpdateDG();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            RepairTicket? ticket = dgData.SelectedItem as RepairTicket;
            Employee? technician = cboTechnician.SelectedItem as Employee;

            if (!decimal.TryParse(txtDeposit.Text.Trim(), out decimal deposit))
            {
                MessageBox.Show("Deposit must be a valid number.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!decimal.TryParse(txtTotalAmount.Text.Trim(), out decimal total))
            {
                MessageBox.Show("Total amount must be a valid number.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TicketStatus status = cboStatus.SelectedItem is TicketStatus s ? s : TicketStatus.Pending;

            bool success = RepairTicketBLL.Instance.UpdateRepairTicket(
                ticket?.ID,
                txtCondition.Text.Trim(),
                txtDiagnosis.Text.Trim(),
                txtNotes.Text.Trim(),
                status,
                total,
                deposit,
                dpAppointment.SelectedDate,
                technician?.ID,
                out string message);

            MessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (success) UpdateDG();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            RepairTicket? ticket = dgData.SelectedItem as RepairTicket;
            if (ticket is null)
            {
                MessageBox.Show("Please select a repair ticket to remove.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var confirm = MessageBox.Show($"Remove ticket {ticket.TicketCode}?", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            bool success = RepairTicketBLL.Instance.RemoveRepairTicket(ticket.ID, out string message);
            MessageBox.Show(message, success ? "Success" : "Error",
                MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (success) { ClearInputs(); UpdateDG(); }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is not RepairTicket selected) return;

            cboDevice.SelectedItem = cboDevice.Items
                .OfType<Device>()
                .FirstOrDefault(d => d.ID == selected.DeviceID);

            txtCondition.Text = selected.Condition;
            txtDiagnosis.Text = selected.Diagnosis;
            txtNotes.Text = selected.Notes;
            cboStatus.SelectedItem = selected.Status;
            dpAppointment.SelectedDate = selected.AppointmentDate;
            txtDeposit.Text = selected.Deposit.ToString("N0");
            txtTotalAmount.Text = selected.TotalAmount.ToString("N0");

            cboTechnician.SelectedItem = cboTechnician.Items
                .OfType<Employee>()
                .FirstOrDefault(emp => emp?.ID == selected.TechnicianID);
        }

        void UpdateDG() => dgData.ItemsSource = RepairTicketBLL.Instance.GetAllRepairTickets();

        void ClearInputs()
        {
            cboDevice.SelectedIndex = 0;
            txtCondition.Text = txtDiagnosis.Text = txtNotes.Text = string.Empty;
            txtDeposit.Text = txtTotalAmount.Text = "0";
            cboStatus.SelectedIndex = 0;
            dpAppointment.SelectedDate = null;
            cboTechnician.SelectedIndex = 0;
        }
    }
}