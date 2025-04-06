using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SeaAngel.Web.ViewModels
{
    public class ViewModelDetPasajero
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Apelldio { get; set; }

        [Required(ErrorMessage = "La cédula es un dato requerido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "La cédula debe tener exactamente 9 dígitos numéricos")]
        public string DocumentoIdentidad { get; set; } = null!;

        [Required(ErrorMessage = "El correo es un dato requerido")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es un dato requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe tener exactamente 8 dígitos numéricos")]
        public string Telefono { get; set; }
    }
}
