using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.Entities;
using Microsoft.EntityFrameworkCore;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal sealed class DeviceBLL : BLLBase
    {
        internal static DeviceBLL Instance { get; } = new DeviceBLL();

        DeviceDAL _dal = new DeviceDAL();
        CustomerDAL _customerDal = new CustomerDAL();

        public List<Device> GetAllDevices()
        {
            CheckAuth();
            return _dal.GetAllDevices();
        }

        public List<Device> GetDevicesByCustomer(Guid customerId)
        {
            CheckAuth();
            return _dal.GetDevicesByCustomer(customerId);
        }

        public Device? GetDevice(Guid id)
        {
            CheckAuth();
            return _dal.GetDevice(id);
        }

        public List<Customer> GetAllCustomers()
        {
            CheckAuth();
            return new CustomerDAL().GetAllCustomers();
        }

        public bool AddDevice(string name, string description, string? serial, string type,
            string? brand, string? model, string? color, string? physicalCondition, string? notes,
            Guid? customerId, out string message)
        {
            CheckAuth();
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Device name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                message = "Device type cannot be empty.";
                return false;
            }
            if (customerId is null || customerId == Guid.Empty)
            {
                message = "Please select a customer.";
                return false;
            }
            Customer? customer = _customerDal.GetCustomer(customerId.Value);
            if (customer is null)
            {
                message = "Selected customer not found.";
                return false;
            }
            try
            {
                Device device = new Device
                {
                    Name = name,
                    Description = description ?? string.Empty,
                    Serial = string.IsNullOrWhiteSpace(serial) ? null : serial.Trim(),
                    Type = type,
                    Brand = string.IsNullOrWhiteSpace(brand) ? null : brand.Trim(),
                    Model = string.IsNullOrWhiteSpace(model) ? null : model.Trim(),
                    Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim(),
                    PhysicalCondition = string.IsNullOrWhiteSpace(physicalCondition) ? null : physicalCondition.Trim(),
                    Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim(),
                    CustomerID = customerId.Value,
                    Customer = customer
                };
                _dal.AddDevice(device);
            }
            catch (Exception ex)
            {
                message = $"Error adding device: {ex.Message}";
                return false;
            }
            message = "Device added successfully.";
            return true;
        }

        public bool UpdateDevice(Guid? id, string name, string description, string? serial, string type,
            string? brand, string? model, string? color, string? physicalCondition, string? notes,
            Guid? customerId, out string message)
        {
            CheckAuth();
            if (id is null)
            {
                message = "Device not found.";
                return false;
            }
            Device? device = GetDevice(id.Value);
            if (device is null)
            {
                message = "Device not found.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Device name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                message = "Device type cannot be empty.";
                return false;
            }
            if (customerId is null || customerId == Guid.Empty)
            {
                message = "Please select a customer.";
                return false;
            }
            Customer? customer = _customerDal.GetCustomer(customerId.Value);
            if (customer is null)
            {
                message = "Selected customer not found.";
                return false;
            }
            try
            {
                device.Name = name;
                device.Description = description ?? string.Empty;
                device.Serial = string.IsNullOrWhiteSpace(serial) ? null : serial.Trim();
                device.Type = type;
                device.Brand = string.IsNullOrWhiteSpace(brand) ? null : brand.Trim();
                device.Model = string.IsNullOrWhiteSpace(model) ? null : model.Trim();
                device.Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim();
                device.PhysicalCondition = string.IsNullOrWhiteSpace(physicalCondition) ? null : physicalCondition.Trim();
                device.Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim();
                device.CustomerID = customerId.Value;
                _dal.UpdateDevice(device);
            }
            catch (Exception ex)
            {
                message = $"Error updating device: {ex.Message}";
                return false;
            }
            message = "Device updated successfully.";
            return true;
        }

        public bool RemoveDevice(Guid? id, out string message)
        {
            CheckAuth();
            if (id is null)
            {
                message = "Device not found.";
                return false;
            }
            try
            {
                _dal.DeleteDevice(id.Value);
            }
            catch (Exception ex)
            {
                message = $"Error removing device: {ex.Message}";
                return false;
            }
            message = "Device removed successfully.";
            return true;
        }
    }
}
