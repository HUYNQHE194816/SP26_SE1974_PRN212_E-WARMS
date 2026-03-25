using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class RepairOrder
{
    public int RepairOrderId { get; set; }

    public int DeviceId { get; set; }

    public int? TechnicianId { get; set; }

    public string Status { get; set; } = null!;

    public string? Diagnostic { get; set; }

    public decimal? LaborCost { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Device Device { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<RepairOrderPart> RepairOrderParts { get; set; } = new List<RepairOrderPart>();

    public virtual Technician? Technician { get; set; }

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
