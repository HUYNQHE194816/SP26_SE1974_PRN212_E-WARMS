using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyRepairCenter.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public virtual ICollection<Device> Devices { get; set; } = [];
}

public class CustomerCfg : EntityCfg<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);
        builder.ToTable("Customer");
        builder.Property(c => c.Name).HasColumnName("customer_name").IsRequired().HasMaxLength(50);
        builder.Property(c => c.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
        builder.Property(c => c.PhoneNumber).HasColumnName("phone_number").IsRequired().HasMaxLength(15);
        builder.Property(c => c.Address).HasColumnName("address").IsRequired().HasMaxLength(200);

        builder.HasIndex(c => c.PhoneNumber);
        builder.HasIndex(c => c.Email);

        builder.HasMany(c => c.Devices)
               .WithOne(d => d.Customer)
               .HasForeignKey(d => d.CustomerID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}