using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyRepairCenter.Entities;

/// <summary>
/// Linh kiện thay thế được sử dụng trong quá trình sửa chữa thiết bị.
/// </summary>
public class SparePart : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal ImportPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int StockQuantity { get; set; }
    public int WarrantyPeriodMonth { get; set; }
}

public class SparePartCfg : EntityCfg<SparePart>
{
    public override void Configure(EntityTypeBuilder<SparePart> builder)
    {
        base.Configure(builder);
        builder.ToTable("SparePart");
        builder.Property(s => s.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
        builder.Property(s => s.SKU).HasColumnName("sku").IsRequired().HasMaxLength(50);
        builder.Property(s => s.ImportPrice).HasColumnName("import_price").IsRequired().HasPrecision(18, 2);
        builder.Property(s => s.SellingPrice).HasColumnName("selling_price").IsRequired().HasPrecision(18, 2);
        builder.Property(s => s.StockQuantity).HasColumnName("stock_quantity").IsRequired();
        builder.Property(s => s.WarrantyPeriodMonth).HasColumnName("warranty_period_month").IsRequired();
    }
}