using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.DTOs
{
    public class FechaDTO
    {
        public int Id { get; set; }

        public int? Idcrucero { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateOnly? FechaInicio { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateOnly? FechaLimitePago { get; set; }

        public virtual CruceroDTO? IdcruceroNavigation { get; set; }

        public virtual List<FechaHabitacionDTO> FechaHabitacion { get; set; } = null!;
    }
}
