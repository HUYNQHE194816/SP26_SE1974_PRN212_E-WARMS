using System.Text.RegularExpressions;
using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal sealed class CustomerBLL : BLLBase
    {
        internal static CustomerBLL Instance { get; } = new CustomerBLL();

        CustomerDAL _dal = new CustomerDAL();

        public List<Customer> GetAllCustomers()
        {
            CheckAuth();
            return _dal.GetAllCustomers();
        }

        public Customer? GetCustomer(Guid id)
        {
            CheckAuth();
            return _dal.GetCustomer(id);
        }

        public bool AddCustomer(string name, string email, string phoneNumber, string address, out string message)
        {
            CheckAuth();
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Customer name cannot be empty.";
                return false;
            }
            if (name.Trim().Length > 50)
            {
                message = "Customer name cannot exceed 50 characters.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                message = "Customer email cannot be empty.";
                return false;
            }
            if (!Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                message = "Please enter a valid email address.";
                return false;
            }
            if (email.Trim().Length > 100)
            {
                message = "Customer email cannot exceed 100 characters.";
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
            if (phoneNumber.Trim().Length > 15)
            {
                message = "Customer phone number cannot exceed 15 characters.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                message = "Customer address cannot be empty.";
                return false;
            }
            if (address.Trim().Length > 200)
            {
                message = "Customer address cannot exceed 200 characters.";
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
            CheckAuth();
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
            CheckAuth();
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
