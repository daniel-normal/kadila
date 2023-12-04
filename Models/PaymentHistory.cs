using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class PaymentHistory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Display(Name = "FECHA DE PAGO")]
    [Required(ErrorMessage = "Seleccione una fecha válida.")]
    [DataType(DataType.Date)]
    public DateOnly? FechaPago { get; set; }

    [Display(Name = "MONTO")]
    [Range(1, uint.MaxValue, ErrorMessage = "El campo Monto debe ser mayor o igual a 1.")]
    [Required(ErrorMessage = "Ingrese un monto.")]
    public uint? Monto { get; set; }

    [Display(Name = "DEUDA")]
    [Required(ErrorMessage = "Seleccione una deuda.")]
    public ulong DeudaId { get; set; }

    [Display(Name = "FECHA DE REGISTRO")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "FECHA DE ACTUALIZACIÓN")]
    public DateTime? UpdatedAt { get; set; }

    [Display(Name = "DEUDA")]
    [Required(ErrorMessage = "Seleccione una deuda.")]
    [ForeignKey("DeudaId")]
    public virtual Debt Deuda { get; set; } = null!;
}
