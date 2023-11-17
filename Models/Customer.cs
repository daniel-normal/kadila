using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kadila.Models
{
    public partial class Customer
    {
        public ulong Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string? Apellido { get; set; }

        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El número de teléfono debe tener entre 10 y 15 dígitos.")]
        public string? Telefono { get; set; }

        [Display(Name = "Saldo/Deuda")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El saldo de deuda debe ser un número entero.")]
        public uint? SaldoDeuda { get; set; }

        [Display(Name = "Tipo")]
        public string? TipoCliente { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Fecha de Actualización")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}