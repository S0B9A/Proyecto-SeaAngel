using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class ReservaComplementosDTO
    {
        [ValidateNever]
        public int Idreserva { get; set; }

        [ValidateNever]
        [Display(Name = "Codigo de complemento")]
        public int Idcomplemento { get; set; }

        [Display(Name = "Complemento")]
        public string NombreComplemento { get; set; } = default!;

        [Display(Name = "Aplicación")]
        public string AplicacionComplemento { get; set; } = default!;

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser un número positivo")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico")]
        [ValidateNever]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser un número positivo")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico")]
        public int Cantidad { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public virtual ComplementosDTO IdcomplementoNavigation { get; set; } = null!;

        [ValidateNever]
        [JsonIgnore]
        public virtual EncReservaDTO IdreservaNavigation { get; set; } = null!;

    }
}
