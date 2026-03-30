using Microsoft.EntityFrameworkCore;
using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.DataAccessLayer
{
    internal class EmployeeDAL
    {
        public List<Employee> GetAllEmployees() =>
            WRCDbCtx.Instance.Employees.AsNoTracking().ToList();

        public Employee? GetEmployee(Guid id) =>
            WRCDbCtx.Instance.Employees.FirstOrDefault(e => e.ID == id);

        public Employee? GetEmployeeByUsername(string username) =>
            WRCDbCtx.Instance.Employees.FirstOrDefault(e => e.Username == username);

        public void AddEmployee(Employee employee)
        {
            WRCDbCtx.Instance.Employees.Add(employee);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            WRCDbCtx.Instance.Employees.Update(employee);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void DeleteEmployee(Guid id)
        {
            var employee = WRCDbCtx.Instance.Employees
                .FirstOrDefault(e => e.ID == id);
            if (employee is null)
                throw new InvalidOperationException($"Employee with ID {id} not found.");
            employee.Deleted = true;
            WRCDbCtx.Instance.SaveChanges();
        }
    }
}