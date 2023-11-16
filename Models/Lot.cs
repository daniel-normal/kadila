using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Lot
{
    public ulong Id { get; set; }

    public string? Nombre { get; set; }

    public uint? Cantidad { get; set; }

    public uint? PrecioActual { get; set; }

    public ulong ProductoId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Producto { get; set; } = null!;
}
