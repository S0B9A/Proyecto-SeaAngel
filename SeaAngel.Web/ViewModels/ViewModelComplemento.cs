using System.ComponentModel.DataAnnotations;

namespace SeaAngel.Web.ViewModels
{
    public class ViewModelComplemento
    {
        [Display(Name = "Complemento")]
        public int IdComplemento { get; set; }

        [Display(Name = "Cantidad")]
        [Range(0, 999999999, ErrorMessage = "Cantidad mínimo es {0}")]
        public int Cantidad { get; set; }
    }
}
