using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class DetReserva
{
    public int IdencReserva { get; set; }

    public int Idhabitacion { get; set; }

    public int? CantidadPasajeros { get; set; }

    public decimal? Precio { get; set; }

    public virtual EncReserva IdencReservaNavigation { get; set; } = null!;

    public virtual Habitacion IdhabitacionNavigation { get; set; } = null!;
}
