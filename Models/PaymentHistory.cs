using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class PaymentHistory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Display(Name = "Fecha de Pago")]
    [DataType(DataType.Date)]
    public DateOnly? FechaPago { get; set; }

    [Range(1, uint.MaxValue, ErrorMessage = "El campo Monto debe ser mayor o igual a 1.")]
    public uint? Monto { get; set; }

    public ulong DeudaId { get; set; }

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("DeudaId")]
    public virtual Debt Deuda { get; set; } = null!;
}
