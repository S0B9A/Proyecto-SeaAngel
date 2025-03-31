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
    public class EncReservaDTO
    {

        [ValidateNever]
        public int Id { get; set; }

        [ValidateNever]
        public int? Idusuario { get; set; }

        public int? Idfecha { get; set; }

        public DateTime? FechaCreacion { get; set; }

        [ValidateNever]
        public DateTime? FechaPago { get; set; }


        [ValidateNever]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Cantidad de pasajeros deber númerico")]
        public string? CantidadDePasajeros { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Cantidad de habitaciones deber númerico")]
        public string? CantidadDeCamarotes { get; set; }


        [ValidateNever]
        public string? PrecioTotalCamorotes { get; set; }


        [ValidateNever]
        public string? Subtotal { get; set; }


        [ValidateNever]
        public string? Impuesto { get; set; }


        [ValidateNever]
        public string? PrecioTotal { get; set; }


        [ValidateNever]
        public virtual UsuarioDTO? IdusuarioNavigation { get; set; }

        [ValidateNever]
        public virtual FechaDTO? IdfechaNavigation { get; set; }

        [ValidateNever]
        public virtual ICollection<ReservaComplementosDTO> ReservaComplementos { get; set; } 
    }
}
