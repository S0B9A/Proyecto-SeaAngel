using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class DetReservaDTO
    {

        [JsonIgnore]
        [Display(Name = "Identificador DetReserva")]
        public int Id { get; set; }

        public int IdencReserva { get; set; }

        [Display(Name = "Codigo Habitación")]
        public int Idhabitacion { get; set; }

        [Display(Name = "Habitación")]
        public string NombreHabitacion { get; set; } = default!;

        [Display(Name = "Cantidad de pasajeros")]
        public int CantidadPasajeros { get; set; }

        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [JsonIgnore]
        public virtual EncReservaDTO IdencReservaNavigation { get; set; } = null!;

        [JsonIgnore]
        public virtual HabitacionDTO IdhabitacionNavigation { get; set; } = null!;
    }
}
