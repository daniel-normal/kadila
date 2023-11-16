using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Product
{
    public ulong Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public uint? Precio { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Lot> Lots { get; set; } = new List<Lot>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
