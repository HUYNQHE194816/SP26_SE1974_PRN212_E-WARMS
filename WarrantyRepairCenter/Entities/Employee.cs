using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyRepairCenter.Entities;

public class Employee : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
    public EmployeeStatus Status { get; set; }

    public virtual ICollection<RepairTicket> RepairTickets { get; set; } = [];
}

public class EmployeeCfg : EntityCfg<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        base.Configure(builder);
        builder.ToTable("Employee");
        builder.Property(e => e.FullName).HasColumnName("full_name").IsRequired().HasMaxLength(100);
        builder.Property(e => e.Username).HasColumnName("username").IsRequired().HasMaxLength(50);
        builder.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired().HasMaxLength(-1);
        builder.Property(e => e.Role).HasColumnName("role").IsRequired();
        builder.Property(e => e.Status).HasColumnName("status").IsRequired();
    }
}