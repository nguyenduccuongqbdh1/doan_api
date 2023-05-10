using System;
using System.Collections.Generic;

namespace DoAn_be.Entities;

public partial class Product
{
    public int IdProduct { get; set; }

    public string NameProduct { get; set; } = null!;

    public string? DetailProduct { get; set; }

    public int QuantityProduct { get; set; }

    public decimal PriceProduct { get; set; }

    public string TagProduct { get; set; } = null!;

    public string IngredientProduct { get; set; } = null!;

    public DateTime DayCreate { get; set; }

    public DateTime Hsd { get; set; }

    public int IdAdmins { get; set; }

    public virtual Admin IdAdminsNavigation { get; set; } = null!;

    public virtual ICollection<ImageProduct> ImageProducts { get; set; } = new List<ImageProduct>();
}
