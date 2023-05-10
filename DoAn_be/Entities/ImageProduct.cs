using System;
using System.Collections.Generic;

namespace DoAn_be.Entities;

public partial class ImageProduct
{
    public int IdImageProduct { get; set; }

    public string ImageNameProduct { get; set; } = null!;

    public int IdProduct { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;
}
