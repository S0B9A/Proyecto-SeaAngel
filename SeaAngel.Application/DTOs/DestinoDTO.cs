using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class DestinoDTO
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public virtual ICollection<PuertoDTO> Puerto { get; set; } = null!;
    }
}
