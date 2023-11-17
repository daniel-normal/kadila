using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class SaleDetail
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    public long? Cantidad { get; set; }

    [Range(1, long.MaxValue, ErrorMessage = "El campo Precio debe ser mayor o igual a 1.")]
    public long? Precio { get; set; }

    public ulong VentaId { get; set; }

    public ulong ProductoId { get; set; }

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductoId")]
    public virtual Product Producto { get; set; } = null!;

    [ForeignKey("VentaId")]
    public virtual Sale Venta { get; set; } = null!;
}
