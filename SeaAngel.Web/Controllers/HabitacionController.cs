﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // GET: HabitacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HabitacionController/Create
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

        // GET: HabitacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HabitacionController/Edit/5
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
