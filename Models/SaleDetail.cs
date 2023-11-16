using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class SaleDetail
{
    public ulong Id { get; set; }

    public long? Cantidad { get; set; }

    public long? Precio { get; set; }

    public ulong VentaId { get; set; }

    public ulong ProductoId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Producto { get; set; } = null!;

    public virtual Sale Venta { get; set; } = null!;
}
