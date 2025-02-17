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
    }
}
