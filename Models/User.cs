using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    [Display(Name = "USUARIO")]
    [Required(ErrorMessage = "Ingrese un nombre de usuario.")]
    public string Name { get; set; } = null!;
    [Display(Name = "CORREO ELECTRÓNICO")]
    [Required(ErrorMessage = "Ingrese un correo electrónico.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Ingrese una contraseña correcta.")]
    [MinLength(6, ErrorMessage = "La longitud mínima de la contraseña es de 8 caracteres.")]
    public string Password { get; set; } = null!;

    [Display(Name = "FECHA DE REGISTRO")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }
    [Display(Name = "Rol")]

    public ulong? RolId { get; set; }

    [ForeignKey("RolId")]
    public virtual Role? Rol { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}