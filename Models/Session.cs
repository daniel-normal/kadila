namespace kadila.Models;
using System.Collections.Generic;

public partial class Session
{
    public ulong Id { get; set; }

    public DateTime LoginDate { get; set; }

    public DateTime? LogoutDate { get; set; }

    public ulong? UserId { get; set; }

    public virtual User? User { get; set; }
}