using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal class EmployeeBLL
    {
        internal static EmployeeBLL Instance { get; } = new EmployeeBLL();

        EmployeeDAL _dal = new EmployeeDAL();

        public List<Employee> GetAllEmployees() => _dal.GetAllEmployees();

        public Employee? GetEmployee(Guid id) => _dal.GetEmployee(id);

        public bool AddEmployee(string fullName, string username, string password, EmployeeRole role, out string message)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                message = "Full name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Username cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                message = "Password cannot be empty.";
                return false;
            }
            if (_dal.GetEmployeeByUsername(username) != null)
            {
                message = $"Username '{username}' is already taken.";
                return false;
            }
            try
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                Employee employee = new Employee
                {
                    FullName = fullName,
                    Username = username,
                    PasswordHash = passwordHash,
                    Role = role
                };
                _dal.AddEmployee(employee);
            }
            catch (Exception ex)
            {
                message = $"Error adding employee: {ex.Message}";
                return false;
            }
            message = "Employee added successfully.";
            return true;
        }

        public bool UpdateEmployee(Guid? id, string fullName, string username, EmployeeRole role, out string message)
        {
            if (id is null)
            {
                message = "Employee not found.";
                return false;
            }
            Employee? employee = GetEmployee(id.Value);
            if (employee is null)
            {
                message = "Employee not found.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(fullName))
            {
                message = "Full name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Username cannot be empty.";
                return false;
            }
            // Check duplicate username (except for current employee)
            Employee? existing = _dal.GetEmployeeByUsername(username);
            if (existing != null && existing.ID != id.Value)
            {
                message = $"Username '{username}' is already taken.";
                return false;
            }
            try
            {
                employee.FullName = fullName;
                employee.Username = username;
                employee.Role = role;
                _dal.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                message = $"Error updating employee: {ex.Message}";
                return false;
            }
            message = "Employee updated successfully.";
            return true;
        }

        public bool ChangePassword(Guid? id, string newPassword, out string message)
        {
            if (id is null)
            {
                message = "Employee not found.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                message = "Password cannot be empty.";
                return false;
            }
            Employee? employee = GetEmployee(id.Value);
            if (employee is null)
            {
                message = "Employee not found.";
                return false;
            }
            try
            {
                employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _dal.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                message = $"Error changing password: {ex.Message}";
                return false;
            }
            message = "Password changed successfully.";
            return true;
        }

        public bool RemoveEmployee(Guid? id, out string message)
        {
            if (id is null)
            {
                message = "Employee not found.";
                return false;
            }
            try
            {
                _dal.DeleteEmployee(id.Value);
            }
            catch (Exception ex)
            {
                message = $"Error removing employee: {ex.Message}";
                return false;
            }
            message = "Employee removed successfully.";
            return true;
        }
    }
}

