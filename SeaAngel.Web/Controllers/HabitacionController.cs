using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Implementations;
using SeaAngel.Application.Services.Interfaces;

namespace SeaAngel.Web.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly IServiceHabitacion _serviceHabitacion;

        public HabitacionController(IServiceHabitacion serviceHabitacion)
        {
            _serviceHabitacion = serviceHabitacion;
        }

        public async Task<IActionResult> GetHabitacionByName(string filtro)
        {

            var collection = await _serviceHabitacion.FindByNameAsync(filtro);
            return Json(collection);

        }

        // GET: HabitacionController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var collection = await _serviceHabitacion.ListAsync();
            return View(collection);
        }

        // GET: HabitacionController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                var @object = await _serviceHabitacion.FindByIdAsync(id.Value);
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

        // GET: Lista de Mantenemiento
        [HttpGet]
        public async Task<IActionResult> Mantenimiento()
        {
            var collection = await _serviceHabitacion.ListAsync();
            return View(collection);
        }

        // GET: HabitacionController/Create
        public async Task<IActionResult> Create()
        {
            var nextReceiptNumber = await _serviceHabitacion.GetNextNumber();
            ViewBag.CurrentReceipt = nextReceiptNumber;

            return View();
        }

        // POST: BarcoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HabitacionDTO dto, IFormFile imageFile)
        {
            MemoryStream target = new MemoryStream();

            try
            {
                if (dto.CapacidadMax <= dto.CapacidadMin)
                {
                    return BadRequest("La capacidad máxima debe ser mayor a la mínima");
                }

                var lista = await _serviceHabitacion.ListAsync();
                var item = lista.FirstOrDefault(o => o.Nombre == dto.Nombre);

                if (item != null)
                {
                    return BadRequest("Nombre existente");

                }
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (dto.Foto == null)
                {
                    if (imageFile != null)
                    {
                        imageFile.OpenReadStream().CopyTo(target);

                        dto.Foto = target.ToArray();
                        ModelState.Remove("Imagen");
                    }
                }

                //Agregar datos faltantes al barco
                dto.ID = 0;

                await _serviceHabitacion.AddAsync(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: HabitacionController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var habitacion = await _serviceHabitacion.FindByIdAsync(id);

            if (habitacion == null)
            {
                return NotFound("La habitación no existe.");
            }

            return View(habitacion);
        }

        // POST: HabitacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HabitacionDTO dto)
        {
            try
            {
                await _serviceHabitacion.UpdateAsync(id, dto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: HabitacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HabitacionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
