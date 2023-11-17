using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kadila.Models;

public partial class Role
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Required(ErrorMessage = "El campo Name es obligatorio.")]
    [StringLength(255, ErrorMessage = "El campo Name no puede tener más de 255 caracteres.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "El campo GuardName es obligatorio.")]
    [StringLength(255, ErrorMessage = "El campo GuardName no puede tener más de 255 caracteres.")]
    public string GuardName { get; set; } = null!;

    [Display(Name = "Fecha de Creación")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Fecha de Actualización")]
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}