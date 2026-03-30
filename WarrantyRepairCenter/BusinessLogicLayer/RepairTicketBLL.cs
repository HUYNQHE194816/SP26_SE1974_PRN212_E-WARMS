using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal class RepairTicketBLL
    {
        internal static RepairTicketBLL Instance { get; } = new RepairTicketBLL();

        RepairTicketDAL _dal = new RepairTicketDAL();

        public List<RepairTicket> GetAllRepairTickets() => _dal.GetAllRepairTickets();

        public RepairTicket? GetRepairTicket(Guid id) => _dal.GetRepairTicket(id);

        public List<RepairTicket> GetRepairTicketsByTechnician(Guid technicianId) =>
            _dal.GetRepairTicketsByTechnician(technicianId);

        public List<RepairTicket> GetRepairTicketsByStatus(TicketStatus status) =>
            _dal.GetRepairTicketsByStatus(status);

        public bool AddRepairTicket(Guid deviceId, string condition, string notes,
            DateTime? appointmentDate, decimal deposit, Guid? technicianId, out string message)
        {
            if (deviceId == Guid.Empty)
            {
                message = "Device must be selected.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(condition))
            {
                message = "Device condition cannot be empty.";
                return false;
            }
            if (deposit < 0)
            {
                message = "Deposit cannot be negative.";
                return false;
            }
            try
            {
                RepairTicket ticket = new RepairTicket
                {
                    DeviceID = deviceId,
                    Condition = condition,
                    Notes = notes ?? string.Empty,
                    Diagnosis = string.Empty,
                    AppointmentDate = appointmentDate,
                    Deposit = deposit,
                    TechnicianID = technicianId,
                    Status = TicketStatus.Pending,
                    TotalAmount = 0,
                    Device = null!   // EF resolves via DeviceID FK
                };
                _dal.AddRepairTicket(ticket);
            }
            catch (Exception ex)
            {
                message = $"Error adding repair ticket: {ex.Message}";
                return false;
            }
            message = "Repair ticket added successfully.";
            return true;
        }

        public bool UpdateRepairTicket(Guid? id, string condition, string diagnosis, string notes,
            TicketStatus status, decimal totalAmount, decimal deposit,
            DateTime? appointmentDate, Guid? technicianId, out string message)
        {
            if (id is null)
            {
                message = "Repair ticket not found.";
                return false;
            }
            RepairTicket? ticket = GetRepairTicket(id.Value);
            if (ticket is null)
            {
                message = "Repair ticket not found.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(condition))
            {
                message = "Device condition cannot be empty.";
                return false;
            }
            if (totalAmount < 0)
            {
                message = "Total amount cannot be negative.";
                return false;
            }
            if (deposit < 0)
            {
                message = "Deposit cannot be negative.";
                return false;
            }
            if (deposit > totalAmount && totalAmount > 0)
            {
                message = "Deposit cannot exceed total amount.";
                return false;
            }
            try
            {
                ticket.Condition = condition;
                ticket.Diagnosis = diagnosis ?? string.Empty;
                ticket.Notes = notes ?? string.Empty;
                ticket.Status = status;
                ticket.TotalAmount = totalAmount;
                ticket.Deposit = deposit;
                ticket.AppointmentDate = appointmentDate;
                ticket.TechnicianID = technicianId;
                _dal.UpdateRepairTicket(ticket);
            }
            catch (Exception ex)
            {
                message = $"Error updating repair ticket: {ex.Message}";
                return false;
            }
            message = "Repair ticket updated successfully.";
            return true;
        }

        public bool UpdateStatus(Guid? id, TicketStatus newStatus, out string message)
        {
            if (id is null)
            {
                message = "Repair ticket not found.";
                return false;
            }
            RepairTicket? ticket = GetRepairTicket(id.Value);
            if (ticket is null)
            {
                message = "Repair ticket not found.";
                return false;
            }
            try
            {
                ticket.Status = newStatus;
                _dal.UpdateRepairTicket(ticket);
            }
            catch (Exception ex)
            {
                message = $"Error updating ticket status: {ex.Message}";
                return false;
            }
            message = $"Ticket status updated to {newStatus}.";
            return true;
        }

        public bool RemoveRepairTicket(Guid? id, out string message)
        {
            if (id is null)
            {
                message = "Repair ticket not found.";
                return false;
            }
            try
            {
                _dal.DeleteRepairTicket(id.Value);
            }
            catch (Exception ex)
            {
                message = $"Error removing repair ticket: {ex.Message}";
                return false;
            }
            message = "Repair ticket removed successfully.";
            return true;
        }
    }
}
