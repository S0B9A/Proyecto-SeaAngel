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
    public class DetPasajeroDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [ValidateNever]
        public int IdencReserva { get; set; }


        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;


        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Apelldio { get; set; }

        [ValidateNever]
        public int Edad { get; set; }


        [Display(Name = "Documento de identidad")]
        [Required(ErrorMessage = "La cédula es un dato requerido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "La cédula debe tener exactamente 9 dígitos numéricos")]
        public string DocumentoIdentidad { get; set; } = null!;


        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El correo es un dato requerido")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string Email { get; set; }


        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El teléfono es un dato requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe tener exactamente 8 dígitos numéricos")]
        public string Telefono { get; set; }

        [ValidateNever]
        public virtual EncReservaDTO? IdencReservaNavigation { get; set; }
    }
}
