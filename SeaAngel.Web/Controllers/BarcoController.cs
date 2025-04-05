using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Models;
using System.Text.Json;

namespace SeaAngel.Web.Controllers
{
    public class BarcoController : Controller
    {
        private readonly IServiceBarco _serviceBarco;
        private readonly IServiceHabitacion _serviceHabitacion;

        public BarcoController(IServiceBarco serviceBarco, IServiceHabitacion serviceHabitacion)
        {
            _serviceBarco = serviceBarco;
            _serviceHabitacion = serviceHabitacion;
        }

        // GET: AutorController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceBarco.ListAsync();
            return View(collection);
        }

        // GET: Lista de Mantenemiento
        [HttpGet]
        public async Task<IActionResult> Mantenimiento()
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
                    return RedirectToAction("Index");
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

        // GET: BarcoController/Create
        public async Task<IActionResult> Create()
        {
            var nextReceiptNumber = await _serviceBarco.GetNextNumberBarco();
            ViewBag.CurrentReceipt = nextReceiptNumber;
            ViewBag.ListHabitacion = await _serviceHabitacion.ListAsync();

            // Clear CarShopping
            TempData["CartShopping"] = null;
            TempData.Keep();

            return View();
        }

        // POST: BarcoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarcoDTO dto, IFormFile imageFile)
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

                // Cuando es Insert Image viene en null porque se pasa diferente
                if (dto.Imagen == null)
                {
                    if (imageFile != null)
                    {
                        imageFile.OpenReadStream().CopyTo(target);

                        dto.Imagen = target.ToArray();
                        ModelState.Remove("Imagen");
                    }
                }


                var lista = JsonSerializer.Deserialize<List<BarcoHabitacionDTO>>(json!)!;

                //Agregar datos faltantes al barco
                dto.Id = 0;
                dto.BarcoHabitacion = lista;

                await _serviceBarco.AddAsync(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Keep Cache data
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }


        // GET: BarcoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var lista = new List<BarcoHabitacionDTO>();
            string json = "";
            var @object = await _serviceBarco.FindByIdAsync(id);
            ViewBag.ListHabitacion = await _serviceHabitacion.ListAsync();

            // Clear CarShopping
            TempData["CartShopping"] = null;
            json = (string)TempData["CartShopping"]!;



            if (@object.BarcoHabitacion != null)
            {
                foreach (BarcoHabitacionDTO barcoHabitacion in @object.BarcoHabitacion )
                {
                    var habitacion = await _serviceHabitacion.FindByIdAsync(barcoHabitacion.Idhabitacion);
                    barcoHabitacion.NombreHabitacion = habitacion.Nombre;
                    //Agregar al carrito de compras
                    lista.Add(barcoHabitacion);
                }
            }


            if (lista != null)
            {
                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();


            return View(@object);
        }

        // POST: BarcoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BarcoDTO dto)
        {

            string json;

            try
            {
                json = (string)TempData["CartShopping"]!;

                if (string.IsNullOrEmpty(json) || json.Trim() == "[]")
                {
                    return BadRequest("No hay habitaciones por agregar");
                }

                var lista = JsonSerializer.Deserialize<List<BarcoHabitacionDTO>>(json!)!;

               // Agregar datos faltantes al barco


                dto.BarcoHabitacion = lista;

                await _serviceBarco.UpdateAsync(id, dto);
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
            BarcoHabitacionDTO barcoHabitacionDTO = new BarcoHabitacionDTO();
            var lista = new List<BarcoHabitacionDTO>();
            string json = "";

            var Habitacion = await _serviceHabitacion.FindByIdAsync(id);

            BarcoHabitacionDTO item = new BarcoHabitacionDTO();

            //Cantidad de item a guardar
            barcoHabitacionDTO.CantDisponible = cantidad;

            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<BarcoHabitacionDTO>>(json!)!;

                //Buscar si existe en la lista de habitaciones
                item = lista.FirstOrDefault(o => o.Idhabitacion == id);
                if (item != null)
                {
                    barcoHabitacionDTO.CantDisponible += cantidad;

                }
            }

            if (item != null && item.CantDisponible != 0)
            {
                //Actualizar cantidad de habitaciones existente
                item.CantDisponible += cantidad;
            }
            else
            {
                barcoHabitacionDTO.Idhabitacion = Habitacion.ID;
                barcoHabitacionDTO.CantDisponible = cantidad;
                barcoHabitacionDTO.NombreHabitacion = Habitacion.Nombre;

                //Agregar al carrito de compras
                lista.Add(barcoHabitacionDTO);

            }

            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailBarcoHabitacion", lista);
        }

        public IActionResult GetBarcoHabitacion()
        {
            List<BarcoHabitacionDTO> lista = new List<BarcoHabitacionDTO>();

            string json = "";

            json = (string)TempData["CartShopping"]!;
            lista = JsonSerializer.Deserialize<List<BarcoHabitacionDTO>>(json!)!;

            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailBarcoHabitacion", lista);
        }

        public IActionResult DeleteHabitacion(int idHabitacion)
        {
            BarcoHabitacionDTO barcoHabitacionDTO = new BarcoHabitacionDTO();
            List<BarcoHabitacionDTO> lista = new List<BarcoHabitacionDTO>();
            string json = "";

            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<BarcoHabitacionDTO>>(json!)!;

                //Eliminar de la lista segun el indice
                int idx = lista.FindIndex(p => p.Idhabitacion == idHabitacion);
                lista.RemoveAt(idx);

                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailBarcoHabitacion", lista);

        }


    }
}
