using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Name { get; set; } = null!;
    [Display(Name = "Correo Electrónico")]
    [Required(ErrorMessage = "El campo Email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo Email no tiene un formato válido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El campo Password es obligatorio.")]
    [MinLength(6, ErrorMessage = "La longitud mínima de la contraseña es 6 caracteres.")]
    public string Password { get; set; } = null!;

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }
    [Display(Name = "Rol")]

    public ulong? RolId { get; set; }

    [ForeignKey("RolId")]
    public virtual Role? Rol { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
