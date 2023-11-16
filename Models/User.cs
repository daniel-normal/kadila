using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class User
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ulong? RolId { get; set; }

    public virtual Role? Rol { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
