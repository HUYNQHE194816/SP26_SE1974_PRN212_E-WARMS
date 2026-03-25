using Microsoft.EntityFrameworkCore;
using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.DataAccessLayer
{
    internal class CustomerDAL
    {
        internal static CustomerDAL Instance { get; } = new CustomerDAL();

        public List<Customer> GetAllCustomers() => WRCDbCtx.Instance.Customers.AsNoTracking().ToList();

        public Customer? GetCustomer(Guid id) => WRCDbCtx.Instance.Customers.AsNoTracking().FirstOrDefault(c => c.ID == id);

        public void AddCustomer(Customer customer)
        {
            WRCDbCtx.Instance.Customers.Add(customer);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            WRCDbCtx.Instance.Customers.Update(customer);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void DeleteCustomer(Guid id)
        {
            Customer customer = GetCustomer(id) ?? throw new InvalidOperationException($"Customer with ID {id} not found.");
            WRCDbCtx.Instance.Customers.Remove(customer);
            WRCDbCtx.Instance.SaveChanges();
        }
    }
}
