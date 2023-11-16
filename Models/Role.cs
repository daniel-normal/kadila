using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Role
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public string GuardName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}