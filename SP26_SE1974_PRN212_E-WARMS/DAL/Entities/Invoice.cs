using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int RepairOrderId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? IssuedAt { get; set; }

    public virtual RepairOrder RepairOrder { get; set; } = null!;
}
