using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class EncReserva
{
    public int Id { get; set; }

    public int? Idusuario { get; set; }

    public int? Idfecha { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaPago { get; set; }

    public string Estado { get; set; } = null!;

    public string? CantidadDePasajeros { get; set; }

    public string? CantidadDeCamarotes { get; set; }

    public string? PrecioTotalCamorotes { get; set; }

    public string? Subtotal { get; set; }

    public string? Impuesto { get; set; }

    public string? PrecioTotal { get; set; }

    public virtual ICollection<DetPasajero> DetPasajero { get; set; } = new List<DetPasajero>();

    public virtual ICollection<DetReserva> DetReserva { get; set; } = new List<DetReserva>();

    public virtual Fecha? IdfechaNavigation { get; set; }

    public virtual Usuario? IdusuarioNavigation { get; set; }

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();

    public virtual ICollection<ReservaComplementos> ReservaComplementos { get; set; } = new List<ReservaComplementos>();
}
