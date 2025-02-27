using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.Services.Interfaces;

namespace SeaAngel.Web.Controllers
{
    public class CruceroController : Controller
    {
        private readonly IServiceCrucero _serviceCrucero;

        public CruceroController(IServiceCrucero serviceCrucero)
        {
            _serviceCrucero = serviceCrucero;
        }

        // GET:EncReservaController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceCrucero.ListAsync();
            return View(collection);
        }

        // GET: EncReservaController/Details/5
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
        // GET: CruceroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CruceroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CruceroController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CruceroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: CruceroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CruceroController/Delete/5
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
