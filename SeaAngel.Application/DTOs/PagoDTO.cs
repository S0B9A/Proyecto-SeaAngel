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
        [RegularExpression(@"^(\d{4} ?){4}$", ErrorMessage = "{0} debe contener solo números")]
        [StringLength(19, MinimumLength = 19, ErrorMessage = "{0} debe tener 16 dígitos.")]
        public string NumeroTarjeta { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de expiración es obligatoria.")]
        public DateOnly FechaExpiracion { get; set; }

        [Required(ErrorMessage = "El código CVV es obligatorio.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "El código CVV debe tener 3 dígitos.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El código CVV debe contener solo números.")]
        public string Cvv { get; set; } = null!;

        [Required(ErrorMessage = "El titular de la tarjeta es obligatorio.")]
        public string TitularTarjeta { get; set; } = null!;

        public virtual EncReserva? IdencReservaNavigation { get; set; }

    }
}
