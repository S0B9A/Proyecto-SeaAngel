using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public record BarcoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int Capacidad { get; set; }

        public byte[]? Imagen { get; set; }

        public int CantidadHabitaciones { get; set; }

        public virtual ICollection<BarcoHabitacionDTO> BarcoHabitacion { get; set; } = null!;
    }
}
