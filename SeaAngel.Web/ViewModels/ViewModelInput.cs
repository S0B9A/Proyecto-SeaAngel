using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeaAngel.Web.ViewModels
{
    public class ViewModelInput
    {

        [Display(Name = "Habitacion")]
        public int Idhabitacion { get; set; }

        [Display(Name = "Cantidad")]
        [Range(0, 999999999, ErrorMessage = "Cantidad mínimo es {0}")]
        public int CantDisponible { get; set; }

       
    }
}
