using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal class CustomerBLL
    {
        internal static CustomerBLL Instance { get; } = new CustomerBLL();

        CustomerDAL _dal = new CustomerDAL();

        public List<Customer> GetAllCustomers() => _dal.GetAllCustomers();

        public Customer? GetCustomer(Guid id) => _dal.GetCustomer(id);

        public bool AddCustomer(string name, string email, string phoneNumber, string address, out string message)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Customer name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                message = "Customer email cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                message = "Customer phone number cannot be empty.";
                return false;
            }
            if (!phoneNumber.All(p => char.IsAsciiDigit(p) || p == '+') 
                || phoneNumber.Count(p => p == '+') > 1
                || (phoneNumber.Contains('+') && !phoneNumber.StartsWith('+')))
            {
                message = "Customer phone number must contain only digits and an optional leading '+'.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                message = "Customer address cannot be empty.";
                return false;
            }
            try
            {
                Customer customer = new Customer
                {
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address
                };
                _dal.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                message = $"Error adding customer: {ex.Message}";
                return false;
            }
            message = "Customer added successfully.";
            return true;
        }

        public bool UpdateCustomer(Guid? id, string name, string email, string phoneNumber, string address, out string message)
        {
            if (id is null)
            {
                message = "Customer not found.";
                return false;
            }
            Customer? customer = GetCustomer(id.Value);
            if (customer is null)
            {
                message = "Customer not found.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Customer name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                message = "Customer email cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                message = "Customer phone number cannot be empty.";
                return false;
            }
            if (!phoneNumber.All(p => char.IsAsciiDigit(p) || p == '+')
                || phoneNumber.Count(p => p == '+') > 1
                || (phoneNumber.Contains('+') && !phoneNumber.StartsWith('+')))
            {
                message = "Customer phone number must contain only digits and an optional leading '+'.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                message = "Customer address cannot be empty.";
                return false;
            }
            try
            {
                customer.Name = name;
                customer.Email = email;
                customer.PhoneNumber = phoneNumber;
                customer.Address = address;
                _dal.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                message = $"Error updating customer: {ex.Message}";
                return false;
            }
            message = "Customer updated successfully.";
            return true;
        }

        public bool RemoveCustomer(Guid? id, out string message)
        {
            if (id is null)
            {
                message = "Customer not found.";
                return false;
            }
            try
            {
                _dal.DeleteCustomer(id.Value);
            }
            catch (Exception ex)
            {
                message = $"Error removing customer: {ex.Message}";
                return false;
            }
            message = "Customer removed successfully.";
            return true;
        }
    }
}
