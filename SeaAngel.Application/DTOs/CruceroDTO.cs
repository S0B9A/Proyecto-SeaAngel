using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class CruceroDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public byte[]? Foto { get; set; }

        public int CantDias { get; set; }

        public int? Idbarco { get; set; }

        public DateOnly? FechaInicio { get; set; }

        public virtual ICollection<EncReservaDTO> EncReserva { get; set; }

        public virtual ICollection<FechasPreciosDTO> FechasPrecios { get; set; } = null!;

        public virtual BarcoDTO? IdbarcoNavigation { get; set; }

        public virtual ICollection<ItinerarioDTO> Itinerario { get; set; } = null!;
    }
}
