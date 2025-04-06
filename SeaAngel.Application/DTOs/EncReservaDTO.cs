using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string CantidadDeCamarotes { get; set; }


        [ValidateNever]
        public string? PrecioTotalCamorotes { get; set; }


        [ValidateNever]
        public string? Subtotal { get; set; }


        [ValidateNever]
        public string? Impuesto { get; set; }


        [ValidateNever]
        public string? PrecioTotal { get; set; }
        [ValidateNever]
        public string? PrecioPendiente { get; set; }

        public virtual List<DetPasajeroDTO> DetPasajero { get; set; } = null!;

        public virtual List<DetReservaDTO> DetReserva { get; set; } = null!;

        [ValidateNever]
        public virtual FechaDTO? IdfechaNavigation { get; set; }

        [ValidateNever]
        public virtual UsuarioDTO? IdusuarioNavigation { get; set; }

        [ValidateNever]
        public virtual List<PagoDTO> Pago { get; set; } = new List<PagoDTO>(); //Hacer DTO de Pago

        [ValidateNever]
        public virtual List<ReservaComplementosDTO> ReservaComplementos { get; set; } = null!;

        [NotMapped] // Solo si usás Entity Framework y no querés mapearlo a DB
        public PagoDTO NuevoPago { get; set; } = new PagoDTO();
    }
}
