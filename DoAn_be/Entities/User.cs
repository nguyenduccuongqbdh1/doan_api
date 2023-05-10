using System;
using System.Collections.Generic;

namespace DoAn_be.Entities;

public partial class User
{
    public int IdUsers { get; set; }

    public string PasswordUsers { get; set; } = null!;

    public string UsernameUsers { get; set; } = null!;

    public string AddressUsers { get; set; } = null!;

    public string PhoneNumberUsers { get; set; } = null!;

    public string ImagepathUsers { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
