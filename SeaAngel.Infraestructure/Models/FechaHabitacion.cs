using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class FechaHabitacion
{
    public int Idhabitacion { get; set; }

    public int Idfecha { get; set; }

    public decimal? Precio { get; set; }

    public virtual Fecha IdfechaNavigation { get; set; } = null!;

    public virtual Habitacion IdhabitacionNavigation { get; set; } = null!;
}
