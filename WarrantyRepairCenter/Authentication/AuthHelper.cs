using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.Authentication
{
    static class AuthHelper
    {
        static Employee? currentEmployee;

        static DateTime lastTimeAccess;

        public static Employee CurrentEmployee
        {
            get
            {
                if (!IsAuthenticated())
                    throw new InvalidOperationException("Not logged in.");
                return currentEmployee!;
            }
        }

        internal static bool Login(string username, string password, out string failedReason)
        {
            CreateAdminAccountIfNotExist();
            Employee? employee = WRCDbCtx.Instance.Employees.FirstOrDefault(e => e.Username == username);
            if (employee is null)
            {
                failedReason = "Invalid username or password.";
                return false;
            }
            if (employee.Status == EmployeeStatus.Locked)
            {
                failedReason = "Your account is locked. Please contact the administrator.";
                return false;
            }
            if (employee.Status == EmployeeStatus.Terminated)
            {
                failedReason = "Your account is terminated. Please contact the administrator.";
                return false;
            }
            if (!BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash))
            {
                failedReason = "Password does not match.";
                return false;
            }
            lastTimeAccess = DateTime.UtcNow;
            currentEmployee = employee;
            failedReason = string.Empty;
            return true;
        }

        internal static void Logout()
        {
            currentEmployee = null;
        }

        internal static bool IsAuthenticated()
        {
            if (currentEmployee is null)
                return false;
            TimeSpan timeSinceLastAccess = DateTime.UtcNow - lastTimeAccess;
            if (timeSinceLastAccess.TotalMinutes < 15)
            {
                lastTimeAccess = DateTime.UtcNow;
                return true;
            }
            Logout();
            return false;
        }

        static void CreateAdminAccountIfNotExist()
        {
            Employee? admin = WRCDbCtx.Instance.Employees.FirstOrDefault(e => e.Username == "admin");
            if (admin is null)
            {
                admin = new Employee
                {
                    FullName = "Administrator",
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = EmployeeRole.Admin
                };
                WRCDbCtx.Instance.Employees.Add(admin);
                WRCDbCtx.Instance.SaveChanges();
            }
        }

        internal static bool VerifyPassword(string password)
        {
            if (!IsAuthenticated())
                throw new InvalidOperationException("Not logged in.");
            return BCrypt.Net.BCrypt.Verify(password, currentEmployee!.PasswordHash);
        }
    }
}
