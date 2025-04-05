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
        private readonly IServiceFechaHabitacion _serviceFechaHabitacion;

        public EncReservaController(IServiceEncReserva serviceEncReserva, IServiceCrucero serviceCrucero, IServiceFecha serviceFecha, IServiceHabitacion serviceHabitacion, IServiceFechaHabitacion serviceFechaHabitacion)
        {
            _serviceEncReserva = serviceEncReserva;
            _serviceCrucero = serviceCrucero;
            _serviceFecha = serviceFecha;
            _serviceHabitacion = serviceHabitacion;
            _serviceFechaHabitacion = serviceFechaHabitacion;
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


                // Convalidaciones esenciales
                if (lista.Count > 0)
                {
                    // Si la lista de habitaciones supera la cantidad digitada, devolvemos el error
                    if (lista.Count > Convert.ToInt32(dto.CantidadDeCamarotes))
                    {
                        // Keep Cache data
                        TempData.Keep();
                        return BadRequest($"Tu cantidad de habitaciones supera la cantidad digitada.");
                    }

                    // Si el contador es menor a la cantidad digitada, devolvemos el error
                    if (lista.Count < Convert.ToInt32(dto.CantidadDeCamarotes)){
                        // Keep Cache data
                        TempData.Keep();
                        return BadRequest($"Tu cantidad de habitaciones es menor a la cantidad digitada.");
                    }
                }


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


        public async Task<IActionResult> AddHabitacion(int id, int cantidad, int idfecha)
        {
            try
            {
                var lista = new List<DetReservaDTO>();
                var listaDeVerificaciones = new List<DetReservaDTO>();
                string json = "";
                var habitacion = await _serviceHabitacion.FindByIdAsync(id);
                var fechaHabitacion = await _serviceFechaHabitacion.FindByIdHabitacionAsync(habitacion.ID, idfecha);

                //Convalidaciones esenciales



                if (TempData["CartHabitacion"] != null)
                {
                    json = (string)TempData["CartHabitacion"]!;
                    lista = JsonSerializer.Deserialize<List<DetReservaDTO>>(json!)!;
                    listaDeVerificaciones = JsonSerializer.Deserialize<List<DetReservaDTO>>(json!)!;
                }


                var nuevoItem = new DetReservaDTO
                {
                    Idhabitacion = habitacion.ID,
                    NombreHabitacion = habitacion.Nombre,
                    CantidadPasajeros = cantidad,
                    Precio = (decimal)fechaHabitacion.Precio
                };

                listaDeVerificaciones.Add(nuevoItem);

                // Convalidaciones esenciales
                if (listaDeVerificaciones.Count > 0)
                {
                    var contador = 0;

                    foreach (var habitacionDisponible in listaDeVerificaciones)
                    {
                        if (habitacionDisponible.Idhabitacion == habitacion.ID)
                        {
                            contador++;
                            // Si el contador supera la cantidad disponible, devolvemos el error
                            if (contador > fechaHabitacion.CantDisponible)
                            {
                                // Keep Cache data
                                TempData.Keep();
                                return BadRequest($"Tu cantidad de habitaciones: {habitacion.Nombre} supera la cantidad disponible.");
                            }
                        }
                    }
                }

                lista.Add(nuevoItem);


                // Guardar carrito actualizado
                TempData["CartHabitacion"] = JsonSerializer.Serialize(lista);
                TempData.Keep();

                return PartialView("_DetailDetReserva", lista);

            }
            catch (Exception ex)
            {
                // Keep Cache data
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }


        public IActionResult DeleteHabitacion(int idHabitacion, int cantidadPasajeros)
        {
            DetReservaDTO detReservaDTO = new DetReservaDTO();
            List<DetReservaDTO> lista = new List<DetReservaDTO>();
            string json = "";

            if (TempData["CartHabitacion"] != null)
            {
                json = (string)TempData["CartHabitacion"]!;
                lista = JsonSerializer.Deserialize<List<DetReservaDTO>>(json!)!;

                //Eliminar de la lista segun el indice
                int idx = lista.FindIndex(p => p.Idhabitacion == idHabitacion && p.CantidadPasajeros == cantidadPasajeros);
                if (idx != -1)
                {
                    lista.RemoveAt(idx);
                }

                json = JsonSerializer.Serialize(lista);
                TempData["CartHabitacion"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailDetReserva", lista);

        }

    }
}
