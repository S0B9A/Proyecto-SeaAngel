using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class BarcoHabitacionDTO
    {
        public int Idbarco { get; set; }

        public int Idhabitacion { get; set; }

        public int CantDisponible { get; set; }

        public decimal? PrecioHabitacion { get; set; }

        public virtual BarcoDTO IdbarcoNavigation { get; set; } = null!;

        public virtual HabitacionDTO IdhabitacionNavigation { get; set; } = null!;
    }
}
