using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class PaymentHistory
{
    public ulong Id { get; set; }

    public DateOnly? FechaPago { get; set; }

    public uint? Monto { get; set; }

    public ulong DeudaId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Debt Deuda { get; set; } = null!;
}
