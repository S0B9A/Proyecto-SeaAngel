using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeaAngel.Application.DTOs;
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
            //Recibir el mensaje de TempData
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.NotificationMessage = TempData["Mensaje"];

            }

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
            return View();
        }

        // POST: BarcoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarcoDTO dto, IFormFile imageFile)
        {
            MemoryStream target = new MemoryStream();

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

            if (!ModelState.IsValid)
            {
                // Lee del ModelState todos los errores que
                // vienen para el lado del servidor
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                // Respuesta con errores
                return BadRequest(errors);
            }

            await _serviceBarco.AddAsync(dto);

            TempData["Mensaje"] = Util.SweetAlertHelper.Mensaje(
                "Crear Barco",
                "Barco creado", Util.SweetAlertMessageType.success);

            return RedirectToAction("Index");
        }
    }
}
