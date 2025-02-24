using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.Services.Interfaces;

namespace SeaAngel.Web.Controllers
{
    public class BarcoController : Controller
    {
        private readonly IServiceBarco _serviceBarco;

        public BarcoController(IServiceBarco serviceBarco)
        {
            _serviceBarco = serviceBarco;
        }


        // GET: AutorController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection);
        }


        // GET: BarcoController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                var @object = await _serviceBarco.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Barco no existente");

                }

                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
