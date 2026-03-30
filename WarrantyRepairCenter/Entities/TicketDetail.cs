using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyRepairCenter.Entities;

public class TicketDetail : BaseEntity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Guid RepairTicketID { get; set; }
    public virtual required RepairTicket RepairTicket { get; set; }
    
    public Guid? SparePartID { get; set; }
    public virtual SparePart? SparePart { get; set; }
    
    public Guid? ServiceItemID { get; set; }
    public virtual ServiceItem? ServiceItem { get; set; }
    
    public bool IsWarranty => UnitPrice == 0;
    public decimal TotalPrice => Quantity * UnitPrice;
}

public class TicketDetailCfg : EntityCfg<TicketDetail>
{
    public override void Configure(EntityTypeBuilder<TicketDetail> builder)
    {
        base.Configure(builder);
        builder.ToTable("TicketDetail");
        builder.Property(td => td.Quantity).HasColumnName("quantity").IsRequired();
        builder.Property(td => td.UnitPrice).HasColumnName("unit_price").IsRequired().HasPrecision(18, 2);
        builder.Property(td => td.RepairTicketID).HasColumnName("repair_ticket_id").IsRequired();
        builder.Property(td => td.SparePartID).HasColumnName("spare_part_id").IsRequired(false);
        builder.Property(td => td.ServiceItemID).HasColumnName("service_item_id").IsRequired(false);
        builder.HasOne(td => td.RepairTicket)
            .WithMany(rt => rt.TicketDetails)
            .HasForeignKey(td => td.RepairTicketID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(td => td.SparePart)
            .WithMany()
            .HasForeignKey(td => td.SparePartID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(td => td.ServiceItem)
            .WithMany()
            .HasForeignKey(td => td.ServiceItemID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}