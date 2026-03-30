using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;
using Microsoft.EntityFrameworkCore;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal class DeviceBLL
    {
        internal static DeviceBLL Instance { get; } = new DeviceBLL();

        public List<Device> GetAllDevices() =>
            WRCDbCtx.Instance.Devices
                .AsNoTracking()
                .Include(d => d.Customer)
                .ToList();
    }
}
