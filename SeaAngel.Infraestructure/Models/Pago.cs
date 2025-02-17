using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int? IdencReserva { get; set; }

    public decimal Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string NumeroTarjeta { get; set; } = null!;

    public DateOnly FechaExpiracion { get; set; }

    public string Cvv { get; set; } = null!;

    public string TitularTarjeta { get; set; } = null!;

    public virtual EncReserva? IdencReservaNavigation { get; set; }
}
