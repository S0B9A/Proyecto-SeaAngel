using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Implementations;
using SeaAngel.Application.Services.Interfaces;
using System.Text.Json;

namespace SeaAngel.Web.Controllers
{
    public class FechaController : Controller
    {
        private readonly IServiceFecha _serviceFecha;
        private readonly IServiceBarco _serviceBarco;
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceHabitacion _serviceHabitacion;
        private readonly IServiceBarcoHabitacion _serviceBarcoHabitacion;
        public FechaController(IServiceFecha serviceFecha, IServiceBarco serviceBarco,
            IServiceCrucero serviceCrucero, IServiceHabitacion serviceHabitacion, IServiceBarcoHabitacion serviceBarcoHabitacion)
        {
            _serviceFecha = serviceFecha;
            _serviceBarco = serviceBarco;
            _serviceCrucero = serviceCrucero;
            _serviceHabitacion = serviceHabitacion;
            _serviceBarcoHabitacion = serviceBarcoHabitacion;
        }

        public async Task<IActionResult> Create()
        {
            var IDCrucero = await _serviceCrucero.GetNextNumber();
            var objeto = await _serviceCrucero.FindByIdAsync(IDCrucero);

            var IDFecha = await _serviceFecha.GetNextNumber();

            ViewBag.IDFecha = IDFecha;
            ViewBag.ListHabitaciones = await _serviceBarco.ListHabitaciones((int)objeto.Idbarco);

            TempData["CartShopping"] = null;
            TempData.Keep();

            return View();
        }

        // POST: BarcoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FechaDTO dto)
        {

            MemoryStream target = new MemoryStream();
            string json;

            try
            {
                json = (string)TempData["CartShopping"]!;

                if (string.IsNullOrEmpty(json) || json.Trim() == "[]")
                {
                    return BadRequest("No hay habitaciones por agregar");
                }

                var lista = JsonSerializer.Deserialize<List<FechaHabitacionDTO>>(json!)!;

                
                
                if (dto.FechaInicio < dto.FechaLimitePago)
                {
                    TempData.Keep();
                    return BadRequest("La fecha inicial debe ser mayor a la de limite de pago");
                }
                var IDCrucero = await _serviceCrucero.GetNextNumber();
                var objeto = await _serviceCrucero.FindByIdAsync(IDCrucero);
                var lista2= await _serviceBarco.ListHabitaciones((int)objeto.Idbarco);

                if (lista.Count<lista2.Count)
                {
                    TempData.Keep();
                    return BadRequest("Debe asignarle un precio a todas las habitaciones");
                }

                //Asignarle la cantidad disponible a las habitaciones en la tabla fechahabitacion para el stock
                foreach (var item in lista)
                {
                    var BarcoID = objeto.Idbarco;
                    var HabitacionID = item.Idhabitacion;

                    var Habitacion = await _serviceBarcoHabitacion.FindByIdAsync(BarcoID, HabitacionID);

                    item.CantDisponible = Habitacion.CantDisponible;
                }

                dto.Id = 0;
                dto.Idcrucero = IDCrucero;
                dto.FechaHabitacion = lista;

                await _serviceFecha.AddAsync(dto);

                return RedirectToAction();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> AddHabitacion(int id, int precio)
        {
            FechaHabitacionDTO fechaHabitacionDTO = new FechaHabitacionDTO();
            var lista = new List<FechaHabitacionDTO>();
            string json = "";

            var Habitacion = await _serviceHabitacion.FindByIdAsync(id);

            FechaHabitacionDTO item = new FechaHabitacionDTO();

            //Cantidad de item a guardar
            fechaHabitacionDTO.Precio = precio;

            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<FechaHabitacionDTO>>(json!)!;
                    
                //Buscar si existe en la lista de habitaciones
                item = lista.FirstOrDefault(o => o.Idhabitacion == id);

                if (item != null)
                { 
                    fechaHabitacionDTO.Precio += precio;

                }
            }

            if (item != null && item.Precio != 0 && item.Precio != null)
            {
                //Actualizar cantidad de habitaciones existente
                item.Precio += precio;
            }
            else
            {
                fechaHabitacionDTO.Idhabitacion = Habitacion.ID;
                fechaHabitacionDTO.Precio = precio;
                fechaHabitacionDTO.NombreHabitacion = Habitacion.Nombre;


                //Agregar al carrito de compras
                lista.Add(fechaHabitacionDTO);

            }

            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailFechaHabitacion", lista);
        }

        public IActionResult GetFechaHabitacion()
        {
            List<FechaHabitacionDTO> lista = new List<FechaHabitacionDTO>();

            string json = "";

            json = (string)TempData["CartShopping"]!;
            lista = JsonSerializer.Deserialize<List<FechaHabitacionDTO>>(json!)!;

            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailFechaHabitacion", lista);
        }

        public IActionResult DeleteHabitacion(int idHabitacion)
        {
            FechaHabitacionDTO fechaHabitacionDTO = new FechaHabitacionDTO();
            List<FechaHabitacionDTO> lista = new List<FechaHabitacionDTO>();
            string json = "";

            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<FechaHabitacionDTO>>(json!)!;

                //Eliminar de la lista segun el indice
                int idx = lista.FindIndex(p => p.Idhabitacion == idHabitacion);
                lista.RemoveAt(idx);

                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailFechaHabitacion", lista);

        }
    }
}
