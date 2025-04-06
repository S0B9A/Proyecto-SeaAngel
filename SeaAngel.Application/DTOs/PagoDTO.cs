using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
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

        public string NumeroTarjeta { get; set; } = null!;

        public DateOnly FechaExpiracion { get; set; }

        public string Cvv { get; set; } = null!;

        public string TitularTarjeta { get; set; } = null!;

        public virtual EncReserva? IdencReservaNavigation { get; set; }
    }
}
