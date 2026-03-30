using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarrantyRepairCenter.DBContext;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.DataAccessLayer
{
    internal class SparePartDAL
    {
        public List<SparePart> GetAllSpareParts() => WRCDbCtx.Instance.SpareParts.AsNoTracking().ToList();

        public SparePart? GetSparePart(Guid id) => WRCDbCtx.Instance.SpareParts.FirstOrDefault(sp => sp.ID == id);

        public void AddSparePart(SparePart sparePart)
        {
            WRCDbCtx.Instance.SpareParts.Add(sparePart);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void UpdateSparePart(SparePart sparePart)
        {
            WRCDbCtx.Instance.SpareParts.Update(sparePart);
            WRCDbCtx.Instance.SaveChanges();
        }

        public void DeleteSparePart(Guid id)
        {
            SparePart sparePart = GetSparePart(id) ?? throw new InvalidOperationException($"Spare part with ID {id} not found.");
            sparePart.Deleted = true;
            WRCDbCtx.Instance.SpareParts.Update(sparePart);
            WRCDbCtx.Instance.SaveChanges();
        }
    }
}
