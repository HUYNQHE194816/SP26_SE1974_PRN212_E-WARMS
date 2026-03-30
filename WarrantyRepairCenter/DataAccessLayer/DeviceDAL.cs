using Microsoft.EntityFrameworkCore;
using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.DataAccessLayer
{
    internal class DeviceDAL
    {
        public List<Device> GetAllDevices() => WRCDbCtx.Instance.Devices
            .Include(d => d.Customer)
            .AsNoTracking()
            .ToList();

        public List<Device> GetDevicesByCustomer(Guid customerId) => WRCDbCtx.Instance.Devices
            .Include(d => d.Customer)
            .Where(d => d.CustomerID == customerId)
            .ToList();

        public Device? GetDevice(Guid id) => WRCDbCtx.Instance.Devices
            .Include(d => d.Customer)
            .FirstOrDefault(d => d.ID == id);

        public void AddDevice(Device device)
        {
            WRCDbCtx.Instance.Devices.Add(device);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void UpdateDevice(Device device)
        {
            WRCDbCtx.Instance.Devices.Update(device);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void DeleteDevice(Guid id)
        {
            Device device = GetDevice(id) ?? throw new InvalidOperationException($"Device with ID {id} not found.");
            device.Deleted = true;
            WRCDbCtx.Instance.Devices.Update(device);
            WRCDbCtx.Instance.SaveChanges();
        }
    }
}
