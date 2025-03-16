using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class ItinerarioDTO
    {

        public int? Idcrucero { get; set; }

        public int? Idpuerto { get; set; }

        public int Dia { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual CruceroDTO? IdcruceroNavigation { get; set; } = null!;

        public virtual PuertoDTO? IdpuertoNavigation { get; set; } = null!;
    }
}
