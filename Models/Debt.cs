using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Debt
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Display(Name = "FECHA LÍMITE")]
    [Required(ErrorMessage = "Seleccione una fecha.")]
    [DataType(DataType.Date)]
    public DateOnly? FechaLimite { get; set; }

    [Display(Name = "FECHA DE CREACIÓN")]
    [Required(ErrorMessage = "Seleccione una fecha.")]
    [DataType(DataType.Date)]
    public DateOnly? FechaCreacion { get; set; }

    [Display(Name = "MONTO")]
    [Required(ErrorMessage = "El monto es obligatorio.")]
    [Range(1, uint.MaxValue, ErrorMessage = "El campo Monto debe ser mayor o igual a 1.")]
    public uint? Monto { get; set; }

    [Display(Name = "ESTADO")]
    [StringLength(50, ErrorMessage = "El campo Estado no puede tener más de 50 caracteres.")]
    public string? Estado { get; set; } = "PENDIENTE";

    [Display(Name = "CLIENTE")]
    [Required(ErrorMessage = "Seleccione un cliente.")]
    public ulong ClienteId { get; set; }

    [Display(Name = "FECHA DE REGISTRO")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "FECHA DE ACTUALIZACIÓN")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClienteId")]
    public virtual Customer Cliente { get; set; } = null!;

    public virtual ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
