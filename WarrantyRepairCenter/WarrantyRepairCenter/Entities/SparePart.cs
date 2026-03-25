using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class SparePart
{
    public int PartId { get; set; }

    public string PartName { get; set; } = null!;

    public decimal Price { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<RepairOrderPart> RepairOrderParts { get; set; } = new List<RepairOrderPart>();
}
