using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class RepairOrderPart
{
    public int Id { get; set; }

    public int RepairOrderId { get; set; }

    public int PartId { get; set; }

    public int Quantity { get; set; }

    public virtual SparePart Part { get; set; } = null!;

    public virtual RepairOrder RepairOrder { get; set; } = null!;
}
