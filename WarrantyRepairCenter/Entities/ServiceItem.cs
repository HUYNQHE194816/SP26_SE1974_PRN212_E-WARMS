using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyRepairCenter.Entities;

/// <summary>
/// Dịch vụ sửa chữa được cung cấp.
/// </summary>
public class ServiceItem : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class ServiceItemCfg : EntityCfg<ServiceItem>
{
    public override void Configure(EntityTypeBuilder<ServiceItem> builder)
    {
        base.Configure(builder);
        builder.ToTable("ServiceItem");
        builder.Property(s => s.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
        builder.Property(s => s.Description).HasColumnName("description").IsRequired().HasMaxLength(-1);
        builder.Property(s => s.BasePrice).HasColumnName("base_price").IsRequired().HasPrecision(18, 2);
    }
}