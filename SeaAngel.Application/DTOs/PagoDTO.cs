using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.DTOs
{
    public class PagoDTO
    {
        public int Id { get; set; }

        public int? IdencReserva { get; set; }

        public decimal Monto { get; set; }

        public DateTime? FechaPago { get; set; }

        public string MetodoPago { get; set; } = null!;

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "{0} debe tener entre 13 y 16 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo puede contener números.")]
        public string NumeroTarjeta { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de expiración es obligatoria.")]
        public DateOnly FechaExpiracion { get; set; }

        [Required(ErrorMessage = "El código CVV es obligatorio.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "El código CVV debe tener entre 3 y 4 dígitos.")]
        public string Cvv { get; set; } = null!;

        [Required(ErrorMessage = "El titular de la tarjeta es obligatorio.")]
        public string TitularTarjeta { get; set; } = null!;

        public virtual EncReserva? IdencReservaNavigation { get; set; }

    }
}
