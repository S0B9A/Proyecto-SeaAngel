using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Itinerario
{
    public int Id { get; set; }

    public int? Idcrucero { get; set; }

    public int? Idpuerto { get; set; }

    public int Dia { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual Crucero? IdcruceroNavigation { get; set; }

    public virtual Puerto? IdpuertoNavigation { get; set; }
}
