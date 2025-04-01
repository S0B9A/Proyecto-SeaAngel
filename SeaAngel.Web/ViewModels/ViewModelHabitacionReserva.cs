using System.ComponentModel.DataAnnotations;

namespace SeaAngel.Web.ViewModels
{
    public class ViewModelHabitacionReserva
    {

        [Display(Name = "Habitacion")]
        public int Idhabitacion { get; set; }

        [Display(Name = "Cantidad")]
        [Range(0, 999999999, ErrorMessage = "Cantidad mínimo es {0}")]
        public int CantidadPasajeros { get; set; }

    }
}
