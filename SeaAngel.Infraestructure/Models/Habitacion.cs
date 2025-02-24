using System;
using System.Collections.Generic;

namespace SeaAngel.Infraestructure.Models;

public partial class Habitacion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int CapacidadMin { get; set; }

    public int CapacidadMax { get; set; }

    public decimal TamanoM2 { get; set; }

    public virtual ICollection<BarcoHabitacion> BarcoHabitacion { get; set; } = new List<BarcoHabitacion>();

    public virtual ICollection<DetReserva> DetReserva { get; set; } = new List<DetReserva>();

    public virtual ICollection<FechasPrecios> FechasPrecios { get; set; } = new List<FechasPrecios>();
}
