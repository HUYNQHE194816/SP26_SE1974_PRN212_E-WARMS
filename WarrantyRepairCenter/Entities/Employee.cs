using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyRepairCenter.Entities;

public class Employee : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; }

    public virtual ICollection<RepairTicket> RepairTickets { get; set; } = [];
}

public class EmployeeCfg : EntityCfg<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        base.Configure(builder);
        builder.ToTable("Employee");
        builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Username).IsRequired().HasMaxLength(50);
        builder.Property(e => e.PasswordHash).IsRequired().HasMaxLength(-1);
        builder.Property(e => e.Role).IsRequired();
    }
}