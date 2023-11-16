using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Sale
{
    public ulong Id { get; set; }

    public DateOnly? FechaVenta { get; set; }

    public string? TipoVenta { get; set; }

    public ulong EmpleadoId { get; set; }

    public ulong ClienteId { get; set; }

    public ulong DeudaId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Customer Cliente { get; set; } = null!;

    public virtual Debt Deuda { get; set; } = null!;

    public virtual User Empleado { get; set; } = null!;

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
