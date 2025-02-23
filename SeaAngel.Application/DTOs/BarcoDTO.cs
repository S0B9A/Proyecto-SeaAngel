using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.DTOs
{
    public class BarcoDTO
    {

        public int ID { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public byte[]? Imagen { get; set; } = null;

        public int CantidadHabitaciones { get; set; } // Nueva propiedad calculada

    }
}
