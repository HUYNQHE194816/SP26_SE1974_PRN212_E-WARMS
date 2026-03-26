using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.Authentication
{
    static class AuthHelper
    {
        static Employee? currentEmployee;

        static DateTime lastTimeAccess;

        internal static bool Login(string username, string password)
        {
            CreateAdminAccountIfNotExist();
            Employee? employee = WRCDbCtx.Instance.Employees.FirstOrDefault(e => e.Username == username);
            if (employee is not null && BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash))
            {
                lastTimeAccess = DateTime.UtcNow;
                currentEmployee = employee;
                return true;
            }
            return false;
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

        internal static bool CheckRole(Role role)
        {
            if (!IsAuthenticated())
                return false;
            return currentEmployee!.Role >= role;
        }

        static void CreateAdminAccountIfNotExist()
        {
            Employee? admin = WRCDbCtx.Instance.Employees.FirstOrDefault(e => e.Username == "admin");
            if (admin is null)
            {
                admin = new Employee
                {
                    FullName = "Admin",
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = Role.Admin
                };
                WRCDbCtx.Instance.Employees.Add(admin);
                WRCDbCtx.Instance.SaveChanges();
            }
        }
    }
}
