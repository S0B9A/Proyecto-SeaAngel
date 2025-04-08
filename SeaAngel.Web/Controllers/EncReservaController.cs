using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
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
        private readonly IServicePago _servicePago;
        private readonly IServiceComplementos _serviceComplementos;

        public EncReservaController(IServiceEncReserva serviceEncReserva, IServiceCrucero serviceCrucero,
            IServiceFecha serviceFecha, IServiceHabitacion serviceHabitacion,
            IServiceFechaHabitacion serviceFechaHabitacion, IServicePago servicePago, IServiceComplementos serviceComplementos)
        {
            _serviceEncReserva = serviceEncReserva;
            _serviceCrucero = serviceCrucero;
            _serviceFecha = serviceFecha;
            _serviceHabitacion = serviceHabitacion;
            _serviceFechaHabitacion = serviceFechaHabitacion;
            _servicePago = servicePago;
            _serviceComplementos = serviceComplementos;
        }

        public async Task<IActionResult> GetComplementoByName(string filtro)
        {

            var collection = await _serviceComplementos.FindByNameAsync(filtro);
            return Json(collection);

        }


        public async Task<IActionResult> GenerarPDF(int id)
        {
            var reserva = await _serviceEncReserva.FindByIdAsync(id);

            return new ViewAsPdf("Factura", reserva)  // "Factura" es la vista para el PDF
            {
                FileName = $"Factura_Reserva_{id}.pdf"
            };
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
            ViewBag.ListComplemento = await _serviceComplementos.ListAsync();


            // Ordenar la lista del itinerario por día
            var crucero = await _serviceCrucero.FindByIdAsync(idcrucero);
            var itinerarioOrdenado = crucero.Itinerario.OrderBy(i => i.Dia).ToList();
            // Obtener el primer y el último puerto
            var primerPuerto = itinerarioOrdenado.FirstOrDefault();
            var ultimoPuerto = itinerarioOrdenado.LastOrDefault();
            // Guardar la información en el ViewBag
            ViewBag.PrimerPuerto = primerPuerto;
            ViewBag.UltimoPuerto = ultimoPuerto;



            // Clear CarShopping
            TempData["CartHabitacion"] = null;
            TempData["CartPasajero"] = null;
            TempData["CartComplemento"] = null;
            TempData.Keep();

            return View();
        }

        // POST: EncReservaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EncReservaDTO dto)
        {
            string json;
            string jsonPasajero;
            string jsonComplemento;

            try
            {
                //Habitacion
                json = (string)TempData["CartHabitacion"]!;

                if (string.IsNullOrEmpty(json) || json.Trim() == "[]")
                {
                    return BadRequest("No hay habitaciones por agregar en la reserva");
                }

                //Pasajero
                jsonPasajero = (string)TempData["CartPasajero"]!;

                if (string.IsNullOrEmpty(jsonPasajero) || jsonPasajero.Trim() == "[]")
                {
                    return BadRequest("No hay pasajeros por agregar en la reserva");
                }

                //Complemento
                jsonComplemento = (string)TempData["CartComplemento"]!;

                //Habitacion
                var lista = JsonSerializer.Deserialize<List<DetReservaDTO>>(json!)!;

                //Pasajero
                var listaPasajero = JsonSerializer.Deserialize<List<DetPasajeroDTO>>(jsonPasajero!)!;


                //complemento
                List<ReservaComplementosDTO> listaComplemento = new List<ReservaComplementosDTO>();

                if (!string.IsNullOrEmpty(jsonComplemento))
                {
                    listaComplemento = JsonSerializer.Deserialize<List<ReservaComplementosDTO>>(jsonComplemento)!;
                }


                // Convalidaciones esenciales para habitaciones
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
                    if (lista.Count < Convert.ToInt32(dto.CantidadDeCamarotes))
                    {
                        // Keep Cache data
                        TempData.Keep();
                        return BadRequest($"Tu cantidad de habitaciones es menor a la cantidad digitada.");
                    }
                }

                // Convalidaciones esenciales para pasajeros
                if (listaPasajero.Count > 0)
                {
                    // Si la lista de pasajeros supera la cantidad digitada, devolvemos el error
                    if (listaPasajero.Count > Convert.ToInt32(dto.CantidadDePasajeros))
                    {
                        // Keep Cache data
                        TempData.Keep();
                        return BadRequest($"Tu cantidad de pasajeros supera la cantidad digitada.");
                    }

                    // Si la lista de pasajeros es menor a la cantidad digitada, devolvemos el error
                    if (listaPasajero.Count < Convert.ToInt32(dto.CantidadDePasajeros))
                    {
                        // Keep Cache data
                        TempData.Keep();
                        return BadRequest($"Tu cantidad de pasajeros es menor a la cantidad digitada.");
                    }

                    var contadorPasajeros = 0;

                    foreach (var habitacion in lista)
                    {
                        contadorPasajeros = contadorPasajeros + Convert.ToInt32(habitacion.CantidadPasajeros);
                    }

                    if (contadorPasajeros != Convert.ToInt32(dto.CantidadDePasajeros))
                    {
                        // Keep Cache data
                        TempData.Keep();
                        return BadRequest($"Has agregado {contadorPasajeros} pasajeros a las habitaciones " +
                            $"en total pero indicaste que el total de pasajeros seria: {dto.CantidadDePasajeros}");
                    }
                }

                //Agregar datos faltantes a la reserva
                dto.Id = 0;
                dto.Idusuario = 1;
                dto.FechaCreacion = DateTime.Today;
                dto.DetReserva = lista;
                dto.DetPasajero = listaPasajero;

                if(listaComplemento.Count > 0){
                    dto.ReservaComplementos = listaComplemento;
                }
             

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

        public async Task<IActionResult> Resumen()
        {
            try
            {
                EncReservaDTO dto = new EncReservaDTO();
                var listaHabitaciones = new List<DetReservaDTO>();
                var listaComplementos = new List<ReservaComplementosDTO>();

                if (TempData["CartHabitacion"] != null)
                {
                    var json = (string)TempData["CartHabitacion"];
                    listaHabitaciones = JsonSerializer.Deserialize<List<DetReservaDTO>>(json) ?? new List<DetReservaDTO>();
                    TempData.Keep("CartHabitacion"); // <- Aquí se conserva para la siguiente petición
                }

                if (TempData["CartComplemento"] != null)
                {
                    var json = (string)TempData["CartComplemento"];
                    listaComplementos = JsonSerializer.Deserialize<List<ReservaComplementosDTO>>(json) ?? new List<ReservaComplementosDTO>();
                    TempData.Keep("CartComplemento"); // <- Aquí se conserva para la siguiente petición
                }

                dto.DetReserva = listaHabitaciones;
                dto.ReservaComplementos = listaComplementos;

                return PartialView("_DetailResumen", dto);
            }
            catch (Exception ex)
            {
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

        public async Task<IActionResult> AddPasajero(string Nombre, string Apelldio, string DocumentoIdentidad, string Email, string Telefono)
        {
            try
            {
                var lista = new List<DetPasajeroDTO>();
                string json = "";


                if (TempData["CartPasajero"] != null)
                {
                    json = (string)TempData["CartPasajero"]!;
                    lista = JsonSerializer.Deserialize<List<DetPasajeroDTO>>(json!)!;
                }

                // Convalidaciones esenciales
                if (lista.Count > 0)
                {
                    int idx = lista.FindIndex(p => p.DocumentoIdentidad == DocumentoIdentidad);
                    if (idx != -1)
                    {
                        // Keep Cache data
                        TempData.Keep();
                        return BadRequest($"Ya existe un pasajero con esa cedula: {DocumentoIdentidad} no puede haber pasajeros con la misma cedula.");
                    }
                }


                var nuevoItem = new DetPasajeroDTO
                {
                    Nombre = Nombre,
                    Apelldio = Apelldio,
                    DocumentoIdentidad = DocumentoIdentidad,
                    Email = Email,
                    Telefono = Telefono,
                    Edad = 20
                };

                lista.Add(nuevoItem);


                // Guardar carrito actualizado
                TempData["CartPasajero"] = JsonSerializer.Serialize(lista);
                TempData.Keep();

                return PartialView("_DetailDetPasajero", lista);

            }
            catch (Exception ex)
            {
                // Keep Cache data
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }

        public IActionResult DeletePasajero(string DocumentoIdentidad)
        {
            DetPasajeroDTO detPasjeroDTO = new DetPasajeroDTO();
            List<DetPasajeroDTO> lista = new List<DetPasajeroDTO>();
            string json = "";

            if (TempData["CartPasajero"] != null)
            {
                json = (string)TempData["CartPasajero"]!;
                lista = JsonSerializer.Deserialize<List<DetPasajeroDTO>>(json!)!;

                //Eliminar de la lista segun el indice
                int idx = lista.FindIndex(p => p.DocumentoIdentidad == DocumentoIdentidad);
                if (idx != -1)
                {
                    lista.RemoveAt(idx);
                }

                json = JsonSerializer.Serialize(lista);
                TempData["CartPasajero"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailDetPasajero", lista);
        }

        public async Task<IActionResult> AddComplemento(int id, int cantidad)
        {
            try
            {
                var complemento = await _serviceComplementos.FindByIdAsync(id);
                if (complemento == null)
                    return NotFound("Complemento no encontrado.");

                var lista = new List<ReservaComplementosDTO>();
                ReservaComplementosDTO itemExistente = null;

                // Recuperar la lista del carrito si existe
                if (TempData["CartComplemento"] != null)
                {
                    var json = (string)TempData["CartComplemento"]!;
                    lista = JsonSerializer.Deserialize<List<ReservaComplementosDTO>>(json)!;
                    itemExistente = lista.FirstOrDefault(o => o.Idcomplemento == id);
                }

                if (itemExistente != null)
                {
                    // Actualizar cantidad y precio si ya existe
                    itemExistente.Cantidad += cantidad;
                    itemExistente.Precio = complemento.Precio * itemExistente.Cantidad;
                }
                else
                {
                    // Agregar nuevo complemento
                    var nuevoItem = new ReservaComplementosDTO
                    {
                        Idcomplemento = complemento.Id,
                        Cantidad = cantidad,
                        NombreComplemento = complemento.Nombre,
                        AplicacionComplemento = complemento.Aplicacion,
                        Precio = complemento.Precio * cantidad
                    };
                    lista.Add(nuevoItem);
                }

                // Guardar lista actualizada en TempData
                TempData["CartComplemento"] = JsonSerializer.Serialize(lista);
                TempData.Keep();

                return PartialView("_DetailReservaComplemento", lista);
            }
            catch (Exception ex)
            {
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }

        public IActionResult DeleteComplemento(int idComplemento)
        {
            ReservaComplementosDTO reservaComplementosDTO = new ReservaComplementosDTO();
            List<ReservaComplementosDTO> lista = new List<ReservaComplementosDTO>();
            string json = "";

            if (TempData["CartComplemento"] != null)
            {
                json = (string)TempData["CartComplemento"]!;
                lista = JsonSerializer.Deserialize<List<ReservaComplementosDTO>>(json!)!;

                //Eliminar de la lista segun el indice
                int idx = lista.FindIndex(p => p.Idcomplemento == idComplemento);
                lista.RemoveAt(idx);

                json = JsonSerializer.Serialize(lista);
                TempData["CartComplemento"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailReservaComplemento", lista);
        }

        public async Task<ActionResult> PagoReserva()
        {
            try
            {
                var @numero = await _serviceEncReserva.GetNextNumberReserva();
                var @object = await _serviceEncReserva.FindByIdAsync(8);

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

        // POST: HabitacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EncReservaDTO dto)
        {
            var @id = await _serviceEncReserva.GetNextNumberReserva();
            EncReservaDTO @object = await _serviceEncReserva.FindByIdAsync(8);

            try
            {
                await _serviceEncReserva.UpdateAsync(8, dto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
