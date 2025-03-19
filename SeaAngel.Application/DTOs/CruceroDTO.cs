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
    public class CruceroDTO
    {
        [Display(Name = "Identificador Crucero")]
        public int Id { get; set; }

        [Display(Name = "Nombre Crucero")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Foto Crucero")]
        public byte[]? Foto { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico")]
        public int CantDias { get; set; }

        [Display(Name = "Barco")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int? Idbarco { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateOnly? FechaInicio { get; set; }

        [ValidateNever]
        public virtual List<EncReservaDTO> EncReserva { get; set; } = null!;

        [ValidateNever]
        public virtual List<FechaDTO> Fecha { get; set; } = null!;

        [ValidateNever]
        public virtual BarcoDTO? IdbarcoNavigation { get; set; } = null!;

        public virtual List<ItinerarioDTO> Itinerario { get; set; } = null!;
    }
}
