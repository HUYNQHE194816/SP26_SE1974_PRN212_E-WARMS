using System.Windows;
using WarrantyRepairCenter.Authentication;
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
            if (username.Length < 4 || username.Length > 20)
            {
                message = "Username must be between 4 and 20 characters long.";
                return false;
            }
            if (password.Length < 8 || password.Length > 20)
            {
                message = "Password must be between 8 and 20 characters long.";
                return false;
            }
            if (username.Contains(' ') || password.Contains(' '))
            {
                message = "Username and password cannot contain spaces.";
                return false;
            }
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit) || !password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                message = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
                return false;
            }
            if (role == EmployeeRole.Admin)
            {
                message = "Cannot assign Admin role to new employee.";
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

        public bool UpdateEmployee(Guid? id, string fullName, string username, EmployeeRole role, EmployeeStatus status, out string message)
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
            if (username.Length < 4 || username.Length > 20)
            {
                message = "Username must be between 4 and 20 characters long.";
                return false;
            }
            if (username.Contains(' '))
            {
                message = "Username cannot contain spaces.";
                return false;
            }
            if (role != EmployeeRole.Admin && employee.Role == EmployeeRole.Admin)
            {
                message = "Cannot change Admin role.";
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
                employee.Status = status;
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
            if (newPassword.Length < 8 || newPassword.Length > 20)
            {
                message = "Password must be between 8 and 20 characters long.";
                return false;
            }
            if (newPassword.Contains(' '))
            {
                message = "Password cannot contain spaces.";
                return false;
            }
            if (!newPassword.Any(char.IsUpper) || !newPassword.Any(char.IsLower) || !newPassword.Any(char.IsDigit) || !newPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                message = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
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
                Employee? employee = GetEmployee(id.Value);
                if (employee?.ID == AuthHelper.CurrentEmployee.ID)
                {
                    message = "Cannot remove currently logged-in employee.";
                    return false;
                }
                if (employee?.Role == EmployeeRole.Admin)
                {
                    message = "Cannot remove Admin.";
                    return false;
                }
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

