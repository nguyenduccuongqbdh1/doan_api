using System;
using System.Collections.Generic;

namespace DoAn_be.Entities;

public partial class Order
{
    public int IdOrders { get; set; }

    public DateTime DayCreate { get; set; }

    public string Statuss { get; set; } = null!;

    public decimal Total { get; set; }

    public int IdUsers { get; set; }

    public virtual ICollection<DetailOrder> DetailOrders { get; set; } = new List<DetailOrder>();

    public virtual User IdUsersNavigation { get; set; } = null!;
}
