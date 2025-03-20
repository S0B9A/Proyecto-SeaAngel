using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;
using System.Text.Json.Serialization;

namespace SeaAngel.Application.DTOs
{
    public class FechaHabitacionDTO
    {
        public int Idhabitacion { get; set; }

        public int Idfecha { get; set; }

        [Display(Name = "Habitación")]
        public string NombreHabitacion { get; set; } = default!;

        public decimal? Precio { get; set; }
        [JsonIgnore]
        public virtual FechaDTO IdfechaNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual HabitacionDTO IdhabitacionNavigation { get; set; } = null!;
    }
}
