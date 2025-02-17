using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class BarcoHabitacion
{
    public int Idbarco { get; set; }

    public int Idhabitacion { get; set; }

    public int CantDisponible { get; set; }

    public decimal? PrecioHabitacion { get; set; }

    public virtual Barco IdbarcoNavigation { get; set; } = null!;

    public virtual Habitacion IdhabitacionNavigation { get; set; } = null!;
}
