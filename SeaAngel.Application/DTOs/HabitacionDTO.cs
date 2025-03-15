using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class HabitacionDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser un número positivo")]
        public int CapacidadMin { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser un número positivo")]
        public int CapacidadMax { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(0.1, double.MaxValue, ErrorMessage = "{0} debe ser un número positivo")]
        public decimal TamanoM2 { get; set; }

        public byte[]? Foto { get; set; }

        public virtual List<BarcoHabitacion> BarcoHabitacion { get; set; } = null!;
    }
}
