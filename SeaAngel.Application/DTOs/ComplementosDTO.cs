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
    public class ComplementosDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser un número positivo")]

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Aplicacion { get; set; } = null!;

        [ValidateNever]
        public virtual List<ReservaComplementosDTO> ReservaComplementos { get; set; } = null!;
    }
}
