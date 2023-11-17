using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Debt
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Display(Name = "Fecha Límite")]
    [DataType(DataType.Date)]
    public DateOnly? FechaLimite { get; set; }

    [Display(Name = "Fecha de Creación")]
    [DataType(DataType.Date)]
    public DateOnly? FechaCreacion { get; set; }

    [Range(1, uint.MaxValue, ErrorMessage = "El campo Monto debe ser mayor o igual a 1.")]
    public uint? Monto { get; set; }

    [StringLength(50, ErrorMessage = "El campo Estado no puede tener más de 50 caracteres.")]
    public string? Estado { get; set; }

    public ulong ClienteId { get; set; }

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClienteId")]
    public virtual Customer Cliente { get; set; } = null!;

    public virtual ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
