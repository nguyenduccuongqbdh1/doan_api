using System;
using System.Collections.Generic;

namespace DoAn_be.Entities;

public partial class Admin
{
    public int IdAdmins { get; set; }

    public string AccountAdmins { get; set; } = null!;

    public string PasswordAdmins { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
