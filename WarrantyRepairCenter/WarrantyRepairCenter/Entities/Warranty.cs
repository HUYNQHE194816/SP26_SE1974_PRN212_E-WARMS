using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Warranty
{
    public int WarrantyId { get; set; }

    public int DeviceId { get; set; }

    public int RepairOrderId { get; set; }

    public int? WarrantyMonths { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public virtual Device Device { get; set; } = null!;

    public virtual RepairOrder RepairOrder { get; set; } = null!;
}
