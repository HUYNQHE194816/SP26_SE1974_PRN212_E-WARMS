using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Technician
{
    public int TechnicianId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? SkillLevel { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RepairOrder> RepairOrders { get; set; } = new List<RepairOrder>();
}
