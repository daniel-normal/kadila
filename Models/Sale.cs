using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class Sale
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Display(Name = "Fecha de Venta")]
    [DataType(DataType.Date)]
    public DateOnly? FechaVenta { get; set; }

    [StringLength(50, ErrorMessage = "El campo TipoVenta no puede tener más de 50 caracteres.")]
    public string? TipoVenta { get; set; }

    public ulong EmpleadoId { get; set; }

    public ulong ClienteId { get; set; }

    public ulong DeudaId { get; set; }

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClienteId")]
    public virtual Customer Cliente { get; set; } = null!;

    [ForeignKey("DeudaId")]
    public virtual Debt Deuda { get; set; } = null!;

    [ForeignKey("EmpleadoId")]
    public virtual User Empleado { get; set; } = null!;

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
