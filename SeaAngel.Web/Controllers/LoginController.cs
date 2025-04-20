using Libreria.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Web.Util;
using System.Security.Claims;
using SeaAngel.Application.Services.Implementations;
using SeaAngel.Application.DTOs;

namespace SeaAngel.Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly IServiceUsuario _serviceUsuario;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IServiceUsuario serviceUsuario, ILogger<LoginController> logger)
        {
            _serviceUsuario = serviceUsuario;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.NotificationMessage = TempData["Mensaje"];
            }
            return View();
        }


        [HttpGet]
        public IActionResult Log()
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.NotificationMessage = TempData["Mensaje"];
            }
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
 
        // POST: BarcoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDTO dto)
        {
          
            try
            {
                
                dto.Id = 0;

                await _serviceUsuario.AddAsync(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(ViewModelLogin viewModelLogin)
        {

            if (!ModelState.IsValid)
            {
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));

                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", $"Error de Acceso {errors}", SweetAlertMessageType.error);

                _logger.LogInformation($"Error en login de {viewModelLogin}, Errores --> {errors}");
                return View("Index");
            }
            //Verificar sí el usuario existe
            var usuarioDTO = await _serviceUsuario.LoginAsync(viewModelLogin.User, viewModelLogin.Password);
            if (usuarioDTO == null)
            {
                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", "Error de Acceso", SweetAlertMessageType.error);
                _logger.LogInformation($"Error en login de {viewModelLogin.User}, Error --> Error en acceso");
                return View("Index");
            }

            //Claim almacena información del usuario como nombre, rol y otros.
            List<Claim> claims = new List<Claim>() {

                new Claim(ClaimTypes.Name, usuarioDTO.Nombre),
                new Claim(ClaimTypes.Role, usuarioDTO.Rol!),
                new Claim(ClaimTypes.Email, usuarioDTO.CorreoElectronico!)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            _logger.LogInformation($"Conexion correcta de {viewModelLogin.User}");
            TempData["Message"] = Util.SweetAlertHelper.Mensaje("Login", "Usuario identificado", SweetAlertMessageType.success);

            return RedirectToAction("Index", "Home");
        }

        /* Solo la usuario conectada puede cerrar sesión */
        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            _logger.LogInformation($"Desconexion correcta de {User!.Identity!.Name}");
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Login");
        }
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
