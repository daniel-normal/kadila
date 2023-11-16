using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Debt
{
    public ulong Id { get; set; }

    public DateOnly? FechaLimite { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public uint? Monto { get; set; }

    public string? Estado { get; set; }

    public ulong ClienteId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Customer Cliente { get; set; } = null!;

    public virtual ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
