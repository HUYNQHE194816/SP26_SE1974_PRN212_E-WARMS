using Microsoft.EntityFrameworkCore;
using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.DataAccessLayer
{
    internal class RepairTicketDAL
    {
        public List<RepairTicket> GetAllRepairTickets() =>
            WRCDbCtx.Instance.RepairTickets
                .AsNoTracking()
                .Include(t => t.Device)
                    .ThenInclude(d => d.Customer)
                .Include(t => t.Technician)
                .ToList();

        public RepairTicket? GetRepairTicket(Guid id) =>
            WRCDbCtx.Instance.RepairTickets
                .AsNoTracking()
                .Include(t => t.Device)
                    .ThenInclude(d => d.Customer)
                .Include(t => t.Technician)
                .FirstOrDefault(t => t.ID == id);

        public List<RepairTicket> GetRepairTicketsByTechnician(Guid technicianId) =>
            WRCDbCtx.Instance.RepairTickets
                .AsNoTracking()
                .Include(t => t.Device)
                    .ThenInclude(d => d.Customer)
                .Where(t => t.TechnicianID == technicianId)
                .ToList();

        public List<RepairTicket> GetRepairTicketsByStatus(TicketStatus status) =>
            WRCDbCtx.Instance.RepairTickets
                .AsNoTracking()
                .Include(t => t.Device)
                    .ThenInclude(d => d.Customer)
                .Include(t => t.Technician)
                .Where(t => t.Status == status)
                .ToList();

        public void AddRepairTicket(RepairTicket ticket)
        {
            WRCDbCtx.Instance.RepairTickets.Add(ticket);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void UpdateRepairTicket(RepairTicket ticket)
        {
            WRCDbCtx.Instance.RepairTickets.Update(ticket);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void DeleteRepairTicket(Guid id)
        {
            RepairTicket ticket = GetRepairTicket(id) ?? throw new InvalidOperationException($"Repair ticket with ID {id} not found.");
            ticket.Deleted = true;
            WRCDbCtx.Instance.RepairTickets.Update(ticket);
            WRCDbCtx.Instance.SaveChanges();
        }
    }
}
