using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class HabitacionDTO
    {
        public int ID { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int CapacidadMin { get; set; }

        public int CapacidadMax { get; set; }

        public decimal TamanoM2 { get; set; }

        public virtual List<BarcoHabitacion> BarcoHabitacion { get; set; } = null!;
    }
}
