using Microsoft.EntityFrameworkCore;
using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.DataAccessLayer
{
    internal class ServiceItemDAL
    {
        public List<ServiceItem> GetAllServiceItems() => WRCDbCtx.Instance.ServiceItems.AsNoTracking().ToList();

        public ServiceItem? GetServiceItem(Guid id) => WRCDbCtx.Instance.ServiceItems.FirstOrDefault(si => si.ID == id);

        public void AddServiceItem(ServiceItem serviceItem)
        {
            WRCDbCtx.Instance.ServiceItems.Add(serviceItem);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void UpdateServiceItem(ServiceItem serviceItem)
        {
            WRCDbCtx.Instance.ServiceItems.Update(serviceItem);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void DeleteServiceItem(Guid id)
        {
            ServiceItem serviceItem = GetServiceItem(id) ?? throw new InvalidOperationException($"Service item with ID {id} not found.");
            serviceItem.Deleted = true;
            WRCDbCtx.Instance.ServiceItems.Update(serviceItem);
            WRCDbCtx.Instance.SaveChanges();
        }
    }
}
