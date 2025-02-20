using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.DTOs
{
    public record HabitacionDTO
    {
        public int ID { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string CapacidadMin { get; set; } = null!;

        public string CapacidadMax { get; set; } = null!;
        public string TamanoM2 { get; set; } = null!;
    }
}
