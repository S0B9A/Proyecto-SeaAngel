using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.DTOs
{
    public class FechasPreciosDTO
    {
        public int Id { get; set; }

        public int? Idcrucero { get; set; }

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaLimitePago { get; set; }

        public int? Idhabitacion { get; set; }

        public decimal Precio { get; set; }

        public virtual Crucero? IdcruceroNavigation { get; set; }

        public virtual Habitacion? IdhabitacionNavigation { get; set; }
    }
}
