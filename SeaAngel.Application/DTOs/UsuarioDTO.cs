using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        public string Pais { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public string Rol { get; set; } = null!;

        public virtual ICollection<EncReserva> EncReserva { get; set; }
    }
}
