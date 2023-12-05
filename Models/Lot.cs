using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class Lot
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Display(Name = "NOMBRE")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(255, ErrorMessage = "El nombre no puede tener más de 255 caracteres.")]
    public string? Nombre { get; set; }

    [Display(Name = "CANTIDAD")]
    [Required(ErrorMessage = "Seleccione una cantidad.")]
    [Range(1, uint.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 1.")]
    public uint? Cantidad { get; set; }

    [Display(Name = "PRECIO ACTUAL")]
    [Required(ErrorMessage = "Ingrese precio.")]
    [Range(1, uint.MaxValue, ErrorMessage = "El precio actual debe ser mayor o igual a 1.")]
    public uint? PrecioActual { get; set; }

    [Display(Name = "PRODUCTO")]
    [Required(ErrorMessage = "Seleccione un producto.")]
    public ulong ProductoId { get; set; }

    [Display(Name = "FECHA DE REGISTRO")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "FECHA DE ACTUALIZACIÓN")]
    public DateTime? UpdatedAt { get; set; }

    [Display(Name = "PRODUCTO")]
    [ForeignKey("ProductoId")]
    public virtual Product Producto { get; set; } = null!;
}