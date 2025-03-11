using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.DTOs
{
    public class BarcoHabitacionDTO
    {
        public int Idbarco { get; set; }

        public int Idhabitacion { get; set; }

        [Display(Name = "Habitacion")]
        public string NombreHabitacion { get; set; } = default!;

        [Display(Name = "Cantidad")]
        public int CantDisponible { get; set; }

        [ValidateNever]
        public decimal? PrecioHabitacion { get; set; }

        public virtual BarcoDTO IdbarcoNavigation { get; set; } = null!;

        public virtual HabitacionDTO IdhabitacionNavigation { get; set; } = null!;
    }
}
