using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace WarrantyRepairCenter.Entities;

public class RepairTicket : BaseEntity
{
    public Guid TicketCode { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal Deposit { get; set; }

    public Guid DeviceID { get; set; }
    public virtual required Device Device { get; set; }

    public Guid? TechnicianID { get; set; }
    public virtual Employee? Technician { get; set; }

    public virtual ICollection<TicketDetail> TicketDetails { get; set; } = [];
}

public class RepairTicketCfg : EntityCfg<RepairTicket>
{
    public override void Configure(EntityTypeBuilder<RepairTicket> builder)
    {
        base.Configure(builder);
        builder.ToTable("RepairTicket");
        builder.Property(t => t.TicketCode).HasColumnName("ticket_code").IsRequired().HasValueGenerator<GuidValueGenerator>();
        builder.Property(t => t.Status).HasColumnName("status").IsRequired().HasDefaultValue(TicketStatus.Pending);
        builder.Property(t => t.Condition).HasColumnName("condition").IsRequired().HasMaxLength(-1);
        builder.Property(t => t.Diagnosis).HasColumnName("diagnosis").IsRequired().HasMaxLength(-1);
        builder.Property(t => t.Notes).HasColumnName("notes").IsRequired().HasMaxLength(-1);
        builder.Property(t => t.TotalAmount).HasColumnName("total_amount").HasPrecision(18, 2);
        builder.Property(t => t.Deposit).HasColumnName("deposit").HasPrecision(18, 2);
        builder.Property(t => t.DeviceID).HasColumnName("device_id").IsRequired();
        builder.Property(t => t.TechnicianID).HasColumnName("technician_id");
        builder.HasOne(t => t.Device)
            .WithMany(d => d.RepairTickets)
            .HasForeignKey(t => t.DeviceID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Technician)
            .WithMany(e => e.RepairTickets)
            .HasForeignKey(t => t.TechnicianID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(t => t.TicketDetails)
            .WithOne(td => td.RepairTicket)
            .HasForeignKey(td => td.RepairTicketID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(t => t.TicketCode).IsUnique();
    }
}