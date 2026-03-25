using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Device
{
    public int DeviceId { get; set; }

    public int CustomerId { get; set; }

    public string DeviceName { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public string? Notes { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<RepairOrder> RepairOrders { get; set; } = new List<RepairOrder>();

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
