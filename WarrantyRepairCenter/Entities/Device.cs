using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyRepairCenter.Entities;

public class Device : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Serial { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public string? PhysicalCondition { get; set; }
    public string? Notes { get; set; }

    public Guid CustomerID { get; set; }
    public virtual required Customer Customer { get; set; }

    public virtual ICollection<RepairTicket> RepairTickets { get; set; } = [];
}

public class DeviceCfg : EntityCfg<Device>
{
    public override void Configure(EntityTypeBuilder<Device> builder)
    {
        base.Configure(builder);
        builder.ToTable("Device");
        builder.Property(c => c.Name).HasColumnName("device_name").IsRequired().HasMaxLength(100);
        builder.Property(c => c.Description).HasColumnName("description").IsRequired().HasMaxLength(-1);
        builder.Property(c => c.Serial).HasColumnName("serial").HasMaxLength(100);
        builder.Property(c => c.Type).HasColumnName("type").IsRequired().HasMaxLength(100);
        builder.Property(c => c.Brand).HasColumnName("brand").HasMaxLength(100);
        builder.Property(c => c.Model).HasColumnName("model").HasMaxLength(100);
        builder.Property(c => c.Color).HasColumnName("color").HasMaxLength(50);
        builder.Property(c => c.PhysicalCondition).HasColumnName("physical_condition").HasMaxLength(100);
        builder.Property(c => c.Notes).HasColumnName("notes").HasMaxLength(-1);
        builder.Property(c => c.CustomerID).HasColumnName("customer_id").IsRequired();
        builder.HasOne(d => d.Customer)
               .WithMany(c => c.Devices)
               .HasForeignKey(d => d.CustomerID)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(d => d.RepairTickets)
               .WithOne(rt => rt.Device)
               .HasForeignKey(rt => rt.DeviceID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}