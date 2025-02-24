using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class ReservaComplementosDTO
    {
        public int Idreserva { get; set; }

        public int Idcomplemento { get; set; }

        public decimal Precio { get; set; }

        public int? Cantidad { get; set; }

        public virtual ComplementosDTO IdcomplementoNavigation { get; set; } = null!;

        public virtual EncReservaDTO IdreservaNavigation { get; set; } = null!;

    }
}
