using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal sealed class ServiceItemBLL : BLLBase
    {
        internal static ServiceItemBLL Instance { get; } = new ServiceItemBLL();

        ServiceItemDAL _dal = new ServiceItemDAL();

        public List<ServiceItem> GetAllServices()
        {
            CheckAuth();
            return _dal.GetAllServiceItems();
        }

        public ServiceItem? GetService(Guid id)
        {
            CheckAuth();
            return _dal.GetServiceItem(id);
        }

        public bool AddService(string name, string description, decimal price, out string message)
        {
            CheckAuth();
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Service item name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                message = "Service item description cannot be empty.";
                return false;
            }
            if (price <= 0)
            {
                message = "Service item price must be greater than 0.";
                return false;
            }
            try
            {
                ServiceItem serviceItem = new ServiceItem
                {
                    Name = name,
                    Description = description,
                    BasePrice = price
                };
                _dal.AddServiceItem(serviceItem);
                message = "Service item added successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Error adding service item: {ex.Message}";
                return false;
            }
        }

        public bool UpdateService(Guid? id, string name, string description, decimal price, out string message)
        {
            CheckAuth();
            if (id is null)
            {
                message = "Service item not found.";
                return false;
            }
            ServiceItem? serviceItem = _dal.GetServiceItem(id.Value);
            if (serviceItem == null)
            {
                message = "Service item not found.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Service item name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                message = "Service item description cannot be empty.";
                return false;
            }
            if (price <= 0)
            {
                message = "Service item price must be greater than 0.";
                return false;
            }
            try
            {
                serviceItem.Name = name;
                serviceItem.Description = description;
                serviceItem.BasePrice = price;
                _dal.UpdateServiceItem(serviceItem);
                message = "Service item updated successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Error updating service item: {ex.Message}";
                return false;
            }
        }

        public bool RemoveService(Guid? id, out string message)
        {
            CheckAuth();
            if (id is null)
            {
                message = "Service item not found.";
                return false;
            }
            ServiceItem? serviceItem = _dal.GetServiceItem(id.Value);
            if (serviceItem == null)
            {
                message = "Service item not found.";
                return false;
            }
            try
            {
                _dal.DeleteServiceItem(id.Value);
                message = "Service item deleted successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Error deleting service item: {ex.Message}";
                return false;
            }
        }
    }
}
