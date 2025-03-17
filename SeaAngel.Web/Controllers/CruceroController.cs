using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Implementations;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Models;
using System.Text.Json;

namespace SeaAngel.Web.Controllers
{
    public class CruceroController : Controller
    {
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceBarco _serviceBarco;
        private readonly IServicePuerto _servicePuerto;


        public CruceroController(IServiceCrucero serviceCrucero, IServiceBarco serviceBarco, IServicePuerto servicePuerto)
        {
            _serviceCrucero = serviceCrucero;
            _serviceBarco = serviceBarco;
            _servicePuerto = servicePuerto;
        }


        public async Task<IActionResult> GetPuertoByName(string filtro)
        {

            var collection = await _servicePuerto.FindByNameAsync(filtro);
            return Json(collection);

        }

        // GET:CruceroController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var collection = await _serviceCrucero.ListAsync();
                return View(collection);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // GET: Crucero/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                var @object = await _serviceCrucero.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Crucero no existente");

                }
                return View(@object);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: Lista de Mantenemiento
        [HttpGet]
        public async Task<IActionResult> Mantenimiento()
        {
            var collection = await _serviceCrucero.ListAsync();
            return View(collection);
        }


        // GET: CruceroController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ListBarco = await _serviceBarco.ListAsync();
            ViewBag.ListPuerto = await _servicePuerto.ListAsync();
            // Clear CarShopping
            TempData["CartShopping"] = null;
            TempData.Keep();

            return View();
        }


        // POST: CruceroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CruceroDTO dto, IFormFile imageFile)
        {
            MemoryStream target = new MemoryStream();

            string json;

            try
            {

                json = (string)TempData["CartShopping"]!;

                if (string.IsNullOrEmpty(json))
                {
                    TempData.Keep();
                    return BadRequest("No hay puertos en el crucero");
                }


                // Cuando es Insert Image viene en null porque se pasa diferente
                if (dto.Foto == null)
                {
                    if (imageFile != null)
                    {
                        imageFile.OpenReadStream().CopyTo(target);

                        dto.Foto = target.ToArray();
                        ModelState.Remove("Foto");
                    }
                }

                var lista = JsonSerializer.Deserialize<List<ItinerarioDTO>>(json!)!;


                // Verificar si hay al menos dos registros
                if (lista.Count < 2)
                {
                    TempData.Keep();
                    return BadRequest("El crucero debe incluir al menos dos puertos para su creación.");
                }

                //Verificar si la cantidad total dias del crucero corresponde a la cantidad de dias de la lista de puertose
                if(lista.Count != dto.CantDias)
                {
                    TempData.Keep();
                    return BadRequest("Los días del itinerario del crucero deben coincidir con el total indicado");
                }

                //Agregar datos faltantes al crucero
                dto.Id = 0;

                dto.Itinerario = lista;

                await _serviceCrucero.AddAsync(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Keep Cache data
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> AddPuerto(int id, int dia, string descripcion)
        {
            ItinerarioDTO itinerarioDTO = new ItinerarioDTO();
            var lista = new List<ItinerarioDTO>();
            string json = "";

            var Puerto = await _servicePuerto.FindByIdAsync(id);

            ItinerarioDTO item = new ItinerarioDTO();


            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<ItinerarioDTO>>(json!)!;
            }


            itinerarioDTO.Idpuerto = Puerto.Id;
            itinerarioDTO.Dia = dia;
            itinerarioDTO.Descripcion = descripcion;

            //Agregar al carrito de compras
            lista.Add(itinerarioDTO);

            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailItinerario", lista);
        }


        public IActionResult GetItinenario()
        {
            List<ItinerarioDTO> lista = new List<ItinerarioDTO>();

            string json = "";

            json = (string)TempData["CartShopping"]!;
            lista = JsonSerializer.Deserialize<List<ItinerarioDTO>>(json!)!;

            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailItinerario", lista);
        }

        public IActionResult DeletePuerto(int idPuerto)
        {
            ItinerarioDTO itinerarioDTO = new ItinerarioDTO();
            List<ItinerarioDTO> lista = new List<ItinerarioDTO>();
            string json = "";

            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<ItinerarioDTO>>(json!)!;

                // Encontrar el puerto a eliminar
                var puertoAEliminar = lista.FirstOrDefault(p => p.Idpuerto == idPuerto);
                // Obtener el día del puerto antes de eliminarlo
                int diaDelPuerto = puertoAEliminar.Dia;

                //Eliminar de la lista segun el indice
                int idx = lista.FindIndex(p => p.Idpuerto == idPuerto);

                lista.RemoveAt(idx);

                foreach (var item in lista)
                {
                    if(item.Dia > diaDelPuerto)
                    {
                        item.Dia -= 1;
                    }
                }

                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailItinerario", lista);

        }

    }
}
