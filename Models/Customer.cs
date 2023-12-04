using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models
{
    public partial class Customer
    {
        public ulong Id { get; set; }
        [Display(Name = "NOMBRE")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string? Nombre { get; set; }

        [Display(Name = "APELLIDO")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string? Apellido { get; set; }

        [Display(Name = "DIRECCIÓN")]
        public string? Direccion { get; set; }

        [Display(Name = "CONTACTO")]
        [Required(ErrorMessage = "El número de celular/teléfono es obligatorio.")]
        [RegularExpression(@"^\d{7,9}$", ErrorMessage = "El número de teléfono debe tener entre 7 y 9 dígitos.")]
        public string? Telefono { get; set; }

        [Display(Name = "SALDO/DEUDA")]
        [Required(ErrorMessage = "El saldo de la deuda es obligatorio.")]
        public uint? SaldoDeuda { get; set; }

        [Display(Name = "TIPO DE CLIENTE")]
        public string? TipoCliente { get; set; }

        [Display(Name = "FECHA DE REGISTRO")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "FECHA DE ACTUALIZACIÓN")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}