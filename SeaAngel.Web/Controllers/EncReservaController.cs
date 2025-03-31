using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.Services.Implementations;
using SeaAngel.Application.Services.Interfaces;

namespace SeaAngel.Web.Controllers
{
    public class EncReservaController : Controller
    {

        private readonly IServiceEncReserva _serviceEncReserva;
        private readonly IServiceCrucero _serviceCrucero;

        public EncReservaController(IServiceEncReserva serviceEncReserva, IServiceCrucero serviceCrucero)
        {
            _serviceEncReserva = serviceEncReserva;
            _serviceCrucero = serviceCrucero;
        }

        // GET:EncReservaController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceEncReserva.ListAsync();
            return View(collection);
        }

        // GET: EncReservaController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                var @object = await _serviceEncReserva.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Reserva no existente");

                }
                return View(@object);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: EncController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ListCrucero = await _serviceCrucero.ListAsync();
            TempData.Keep();

            return View();
        }
    }
}
