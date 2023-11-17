using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class Lot
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(255, ErrorMessage = "El campo Nombre no puede tener más de 255 caracteres.")]
    public string? Nombre { get; set; }

    [Range(1, uint.MaxValue, ErrorMessage = "El campo Cantidad debe ser mayor o igual a 1.")]
    public uint? Cantidad { get; set; }

    [Range(1, uint.MaxValue, ErrorMessage = "El campo PrecioActual debe ser mayor o igual a 1.")]
    public uint? PrecioActual { get; set; }

    public ulong ProductoId { get; set; }

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductoId")]
    public virtual Product Producto { get; set; } = null!;
}
