using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class FechasPrecios
{
    public int Id { get; set; }

    public int? Idcrucero { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaLimitePago { get; set; }

    public int? Idhabitacion { get; set; }

    public decimal Precio { get; set; }

    public virtual Crucero? IdcruceroNavigation { get; set; }

    public virtual Habitacion? IdhabitacionNavigation { get; set; }
}
