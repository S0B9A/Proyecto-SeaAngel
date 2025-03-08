using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeaAngel.Web.Models;

namespace SeaAngel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ////Recibir Mensaje
            //if (TempData.ContainsKey("Mensaje"))
            //{
            //    ViewBag.NotificationMessage = TempData["Mensaje"];
            //}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> Enviar()
        {
            ////Procesamiento
            //TempData["Mensaje"] = Util.SweetAlertHelper.Mensaje("Enviar", "Mensaje enviado", Util.SweetAlertMessageType.success);
            ////return View();
            //return RedirectToAction("Index");

            // Procesamiento
            TempData["Mensaje"] = "Mensaje enviado correctamente."; // Asegúrate de que el mensaje esté aquí.

            // Devolver la misma vista sin redirigir
            return View("Index"); // Asegúrate de que la vista se llama "Index"
        }

        [HttpGet]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorHandler(string messagesJson)
        {
            var errorMessages = JsonConvert.
                DeserializeObject<ErrorMiddlewareViewModel>(messagesJson);
            ViewBag.ErrorMessages = errorMessages;
            return View("ErrorHandler");
        }
    }
}
