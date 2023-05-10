using System;
using System.Collections.Generic;

namespace DoAn_be.Entities;

public partial class DetailOrder
{
    public int IdDetailOrders { get; set; }

    public int QuantityDetailOrders { get; set; }

    public decimal PriceDetailOrders { get; set; }

    public int IdOrders { get; set; }

    public virtual Order IdOrdersNavigation { get; set; } = null!;
}
