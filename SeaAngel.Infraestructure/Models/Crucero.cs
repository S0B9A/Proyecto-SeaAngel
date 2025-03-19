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

    public virtual ICollection<Fecha> Fecha { get; set; } = new List<Fecha>();

    public virtual Barco? IdbarcoNavigation { get; set; }

    public virtual ICollection<Itinerario> Itinerario { get; set; } = new List<Itinerario>();
}
