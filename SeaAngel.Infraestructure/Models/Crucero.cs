using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Crucero
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte[]? Foto { get; set; }

    public int CantDias { get; set; }

    public int? Idbarco { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public virtual ICollection<EncReserva> EncReserva { get; set; } = new List<EncReserva>();

    public virtual ICollection<FechasPrecios> FechasPrecios { get; set; } = new List<FechasPrecios>();

    public virtual Barco? IdbarcoNavigation { get; set; }

    public virtual ICollection<Itinerario> Itinerario { get; set; } = new List<Itinerario>();
}
