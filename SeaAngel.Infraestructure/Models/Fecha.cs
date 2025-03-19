using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Fecha
{
    public int Id { get; set; }

    public int? Idcrucero { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaLimitePago { get; set; }

    public virtual ICollection<FechaHabitacion> FechaHabitacion { get; set; } = new List<FechaHabitacion>();

    public virtual Crucero? IdcruceroNavigation { get; set; }
}
