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
    public record BarcoDTO
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico")]
        public int Capacidad { get; set; }

        [Display(Name = "Imagen Barco")]
        public byte[]? Imagen { get; set; }

        public virtual List<BarcoHabitacionDTO> BarcoHabitacion { get; set; } = null!;
    }
}
