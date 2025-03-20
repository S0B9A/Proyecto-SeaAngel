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
        public FechaController(IServiceFecha serviceFecha, IServiceBarco serviceBarco)
        {
            _serviceFecha = serviceFecha;
            _serviceBarco = serviceBarco;
        }

        public async Task<IActionResult> Create(int idBarco)
        {
            ViewBag.ListHabitaciones = await _serviceBarco.ListHabitaciones(idBarco);

            // Clear CarShopping
            TempData["CartShopping"] = null;
            TempData.Keep();

            return PartialView("_Create");
        }

        // POST: BarcoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FechaDTO dto)
        {
            return PartialView("_Create");
        }
    }
}
