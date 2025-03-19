using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class EncReservaDTO
    {

        public int Id { get; set; }

        public int? Idusuario { get; set; }

        public int? Idfecha { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaPago { get; set; }

        public string Estado { get; set; } = null!;

        public string? CantidadDePasajeros { get; set; }

        public string? CantidadDeCamarotes { get; set; }

        public string? PrecioTotalCamorotes { get; set; }

        public string? Subtotal { get; set; }

        public string? Impuesto { get; set; }

        public string? PrecioTotal { get; set; }

        public virtual UsuarioDTO? IdusuarioNavigation { get; set; }
        public virtual FechaDTO? IdfechaNavigation { get; set; }
        public virtual ICollection<ReservaComplementosDTO> ReservaComplementos { get; set; } 
    }
}
