using System.Text.Json;
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
        private readonly IServiceHabitacion _serviceHabitacion;

        public EncReservaController(IServiceEncReserva serviceEncReserva, IServiceCrucero serviceCrucero, IServiceFecha serviceFecha, IServiceHabitacion serviceHabitacion)
        {
            _serviceEncReserva = serviceEncReserva;
            _serviceCrucero = serviceCrucero;
            _serviceFecha = serviceFecha;
            _serviceHabitacion = serviceHabitacion;

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


            // Clear CarShopping
            TempData["CartHabitacion"] = null;
            TempData.Keep();

            return View();
        }


        // POST: EncReservaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EncReservaDTO dto)
        {
            string json;

            try
            {
                json = (string)TempData["CartHabitacion"]!;

                if (string.IsNullOrEmpty(json) || json.Trim() == "[]")
                {
                    return BadRequest("No hay habitaciones por agregar en la reserva");
                }

                var lista = JsonSerializer.Deserialize<List<DetReservaDTO>>(json!)!;

                //Agregar datos faltantes a la reserva
                dto.Id = 0;
                dto.Idusuario = 1;
                dto.FechaCreacion = DateTime.Today;
                dto.DetReserva = lista;

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


        public async Task<IActionResult> AddHabitacion(int id, int cantidad)
        {
            DetReservaDTO detReservaDTO = new DetReservaDTO();
            var lista = new List<DetReservaDTO>();
            string json = "";

            var Habitacion = await _serviceHabitacion.FindByIdAsync(id);

            DetReservaDTO item = new DetReservaDTO();

            //Cantidad de item a guardar
            detReservaDTO.CantidadPasajeros = cantidad;

            if (TempData["CartHabitacion"] != null)
            {
                json = (string)TempData["CartHabitacion"]!;
                lista = JsonSerializer.Deserialize<List<DetReservaDTO>>(json!)!;

                //Buscar si existe en la lista de habitaciones
                item = lista.FirstOrDefault(o => o.Idhabitacion == id);
                if (item != null)
                {
                    detReservaDTO.CantidadPasajeros += cantidad;

                }
            }

            if (item != null && item.CantidadPasajeros != 0)
            {
                //Actualizar cantidad de habitaciones existente
                item.CantidadPasajeros += cantidad;
            }
            else
            {
                detReservaDTO.Idhabitacion = Habitacion.ID;
                detReservaDTO.CantidadPasajeros = cantidad;
                detReservaDTO.NombreHabitacion = Habitacion.Nombre;

                //Agregar al carrito de compras
                lista.Add(detReservaDTO);

            }

            json = JsonSerializer.Serialize(lista);
            TempData["CartHabitacion"] = json;
            TempData.Keep();

            return PartialView("_DetailDetReserva", lista);
        }


        public async Task<ActionResult> PagoReserva()
        {
            try
            {
                var @numero = await _serviceEncReserva.GetNextNumberReserva();
                var @object = await _serviceEncReserva.FindByIdAsync(@numero);

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
    }
}
