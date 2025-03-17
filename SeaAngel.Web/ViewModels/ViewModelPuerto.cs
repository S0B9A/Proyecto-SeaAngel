using System.ComponentModel.DataAnnotations;

namespace SeaAngel.Web.ViewModels
{
    public class ViewModelPuerto
    {

        [Display(Name = "Puerto")]
        public int IdPuerto { get; set; }

        [Display(Name = "Descripcion")]
        public int Descripcion { get; set; }

        [Display(Name = "Dia")]
        [Range(0, 999999999, ErrorMessage = "Cantidad mínimo es {0}")]
        public int Dia { get; set; }

    }
}
