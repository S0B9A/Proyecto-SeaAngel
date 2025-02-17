using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class DetPasajero
{
    public int Id { get; set; }

    public int? IdencReserva { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apelldio { get; set; }

    public int Edad { get; set; }

    public string DocumentoIdentidad { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public virtual EncReserva? IdencReservaNavigation { get; set; }
}
