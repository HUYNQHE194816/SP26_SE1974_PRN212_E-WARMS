using WarrantyRepairCenter.DataAccessLayer;
using WarrantyRepairCenter.Entities;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal sealed class SparePartBLL : BLLBase
    {
        internal static SparePartBLL Instance { get; } = new SparePartBLL();

        SparePartDAL _dal = new SparePartDAL();

        public List<SparePart> GetAllSpareParts()
        {
            CheckAuth();
            return _dal.GetAllSpareParts();
        }

        public SparePart? GetSparePart(Guid id)
        {
            CheckAuth();
            return _dal.GetSparePart(id);
        }

        public bool AddSparePart(string name, string sku, decimal importPrice, decimal sellingPrice, int stockQuantity, int warrantyPeriodMonth, out string message)
        {
            CheckAuth();
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Spare part name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sku))
            {
                message = "SKU cannot be empty.";
                return false;
            }
            if (sku.Length != 10)
            {
                message = "SKU must be exactly 10 characters long.";
                return false;
            }
            if (importPrice < 0)
            {
                message = "Import price cannot be negative.";
                return false;
            }
            if (sellingPrice <= 0)
            {
                message = "Selling price must be greater than 0.";
                return false;
            }
            if (stockQuantity < 0)
            {
                message = "Stock quantity cannot be negative.";
                return false;
            }
            if (warrantyPeriodMonth < 0)
            {
                message = "Warranty period cannot be negative.";
                return false;
            }
            try
            {
                SparePart sparePart = new SparePart
                {
                    Name = name,
                    SKU = sku,
                    ImportPrice = importPrice,
                    SellingPrice = sellingPrice,
                    StockQuantity = stockQuantity,
                    WarrantyPeriodMonth = warrantyPeriodMonth
                };
                _dal.AddSparePart(sparePart);
                message = "Spare part added successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Error adding spare part: {ex.Message}";
            }
            return false;
        }

        public bool UpdateSparePart(Guid? id, string name, string sku, decimal importPrice, decimal sellingPrice, int stockQuantity, int warrantyPeriodMonth, out string message)
        {
            CheckAuth();
            if (id is null)
            {
                message = "Spare part not found.";
                return false;
            }
            SparePart? sparePart = _dal.GetSparePart(id.Value);
            if (sparePart == null)
            {
                message = "Spare part not found.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Spare part name cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sku))
            {
                message = "SKU cannot be empty.";
                return false;
            }
            if (sku.Length != 10)
            {
                message = "SKU must be exactly 10 characters long.";
                return false;
            }
            if (importPrice < 0)
            {
                message = "Import price cannot be negative.";
                return false;
            }
            if (sellingPrice <= 0)
            {
                message = "Selling price must be greater than 0.";
                return false;
            }
            if (stockQuantity < 0)
            {
                message = "Stock quantity cannot be negative.";
                return false;
            }
            if (warrantyPeriodMonth < 0)
            {
                message = "Warranty period cannot be negative.";
                return false;
            }
            try
            {
                sparePart.Name = name;
                sparePart.SKU = sku;
                sparePart.ImportPrice = importPrice;
                sparePart.SellingPrice = sellingPrice;
                sparePart.StockQuantity = stockQuantity;
                sparePart.WarrantyPeriodMonth = warrantyPeriodMonth;
                _dal.UpdateSparePart(sparePart);
                message = "Spare part updated successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Error updating spare part: {ex.Message}";
                return false;
            }
        }

        public bool RemoveSparePart(Guid? id, out string message)
        {
            CheckAuth();
            if (id is null)
            {
                message = "Spare part not found.";
                return false;
            }
            SparePart? sparePart = _dal.GetSparePart(id.Value);
            if (sparePart == null)
            {
                message = "Spare part not found.";
                return false;
            }
            try
            {
                _dal.DeleteSparePart(id.Value);
                message = "Spare part removed successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Error removing spare part: {ex.Message}";
            }
            return false;
        }
    }
}
