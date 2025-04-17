using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Implementations;
using SeaAngel.Application.Services.Interfaces;

namespace SeaAngel.Web.Controllers
{
    public class ComplementosController : Controller
    {
        private readonly IServiceComplementos _serviceComplementos;

        public ComplementosController(IServiceComplementos serviceComplementos)
        {
            _serviceComplementos = serviceComplementos;
        }

        // GET: Lista de Mantenemiento
        [HttpGet]
        public async Task<IActionResult> Mantenimiento()
        {
            var collection = await _serviceComplementos.ListAsync();
            return View(collection);
        }

        // GET: ComplementoController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                var @object = await _serviceComplementos.FindByIdAsync(id.Value);

                if (@object == null)
                {
                    throw new Exception("complemento no existente");

                }

                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: ComplementosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ComplementosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComplementosDTO dto)
        {
            try
            {
                //Agregar datos faltantes al complemento
                dto.Id = 0;

                await _serviceComplementos.AddAsync(dto);

                return RedirectToAction("Mantenimiento");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
