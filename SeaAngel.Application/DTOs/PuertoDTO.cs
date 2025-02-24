using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class PuertoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Pais { get; set; } = null!;

        public int? Iddestino { get; set; }

        public virtual DestinoDTO? IddestinoNavigation { get; set; } = null!;

        public virtual ICollection<ItinerarioDTO> Itinerario { get; set; } = null!;
    }
}
