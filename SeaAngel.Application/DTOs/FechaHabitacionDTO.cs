using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class FechaHabitacionDTO
    {
        public int Idhabitacion { get; set; }

        public int Idfecha { get; set; }

        public decimal? Precio { get; set; }

        public virtual FechaDTO IdfechaNavigation { get; set; } = null!;

        public virtual HabitacionDTO IdhabitacionNavigation { get; set; } = null!;
    }
}
