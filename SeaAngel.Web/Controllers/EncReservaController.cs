using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Implementations;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Web.Controllers
{
    public class EncReservaController : Controller
    {

        private readonly IServiceEncReserva _serviceEncReserva;
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceFecha _serviceFecha;

        public EncReservaController(IServiceEncReserva serviceEncReserva, IServiceCrucero serviceCrucero, IServiceFecha serviceFecha)
        {
            _serviceEncReserva = serviceEncReserva;
            _serviceCrucero = serviceCrucero;
            _serviceFecha = serviceFecha;
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

        // GET: EncReservaController/Create
        public async Task<IActionResult> Create(int idcrucero, int idfecha)
        {
            ViewBag.ListCrucero = await _serviceCrucero.ListAsync();
            ViewBag.Crucero = await _serviceCrucero.FindByIdAsync(idcrucero);
            ViewBag.fecha = await _serviceFecha.FindByIdAsync(idfecha);

            TempData.Keep();

            return View();
        }

        // POST: EncReservaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EncReservaDTO dto)
        {
            
            try
            {
               
                //Agregar datos faltantes a la reserva
                dto.Id = 0;
                dto.Idusuario = 1;
                dto.FechaCreacion = DateTime.Today;

                await _serviceEncReserva.AddAsync(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Keep Cache data
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }
    }
}
