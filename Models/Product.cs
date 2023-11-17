using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kadila.Models;

public partial class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(255, ErrorMessage = "El campo Nombre no puede tener más de 255 caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(500, ErrorMessage = "El campo Descripcion no puede tener más de 500 caracteres.")]
    public string? Descripcion { get; set; }

    [Range(1, uint.MaxValue, ErrorMessage = "El campo Precio debe ser mayor o igual a 1.")]
    public uint? Precio { get; set; }

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Lot> Lots { get; set; } = new List<Lot>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
