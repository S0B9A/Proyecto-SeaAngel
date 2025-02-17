using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Puerto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public int? Iddestino { get; set; }

    public virtual Destino? IddestinoNavigation { get; set; }

    public virtual ICollection<Itinerario> Itinerario { get; set; } = new List<Itinerario>();
}
