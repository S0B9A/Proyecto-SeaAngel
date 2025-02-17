using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Destino
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Puerto> Puerto { get; set; } = new List<Puerto>();
}
