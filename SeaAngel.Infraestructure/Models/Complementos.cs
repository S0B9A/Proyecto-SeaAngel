using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Complementos
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public string Aplicacion { get; set; } = null!;

    public virtual ICollection<ReservaComplementos> ReservaComplementos { get; set; } = new List<ReservaComplementos>();
}
