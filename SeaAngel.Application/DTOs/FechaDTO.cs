using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.DTOs
{
    public class FechaDTO
    {
        public int Id { get; set; }

        public int? Idcrucero { get; set; }

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaLimitePago { get; set; }

        public virtual CruceroDTO? IdcruceroNavigation { get; set; }

        public virtual List<FechaHabitacionDTO> FechaHabitacion { get; set; } = null!;
    }
}
