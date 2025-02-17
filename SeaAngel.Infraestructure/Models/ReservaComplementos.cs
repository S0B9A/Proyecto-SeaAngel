using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class ReservaComplementos
{
    public int Idreserva { get; set; }

    public int Idcomplemento { get; set; }

    public decimal Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual Complementos IdcomplementoNavigation { get; set; } = null!;

    public virtual EncReserva IdreservaNavigation { get; set; } = null!;
}
