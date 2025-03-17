using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class PuertoDTO
    {
        [Display(Name = "Codigo del Puerto")]
        [ValidateNever]
        public int Id { get; set; }

        [Display(Name = "Nombre Puerto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Nombre Pais")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Pais { get; set; } = null!;

        public int? Iddestino { get; set; }

        public virtual DestinoDTO? IddestinoNavigation { get; set; } = null!;

        public virtual List<ItinerarioDTO> Itinerario { get; set; } = null!;
    }
}
