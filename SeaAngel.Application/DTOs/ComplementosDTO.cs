using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class ComplementosDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string Aplicacion { get; set; } = null!;

        public virtual ICollection<ReservaComplementosDTO> ReservaComplementos { get; set; }
    }
}
