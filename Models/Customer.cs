using System;
using System.Collections.Generic;

namespace kadila.Models;

public partial class Customer
{
    public ulong Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public uint? SaldoDeuda { get; set; }

    public string? TipoCliente { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
