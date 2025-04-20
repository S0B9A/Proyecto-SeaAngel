using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class ItinerarioDTO
    {

        public int Idcrucero { get; set; }

        [Display(Name = "Codigo de puerto")]
        public int Idpuerto { get; set; }

        [Display(Name = "Puerto")]
        public string NombrePuerto { get; set; } = default!;

        public int Dia { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual CruceroDTO? IdcruceroNavigation { get; set; } = null!;

        public virtual PuertoDTO? IdpuertoNavigation { get; set; } = null!;
    }
}
