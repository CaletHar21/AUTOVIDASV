using AutoVidaSv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;

namespace AutoVidaSv.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AutovidasvContext _context;

        public HomeController(ILogger<HomeController> logger, AutovidasvContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        // POST: /Home/ProcesarLogin
        [HttpPost]
        public ActionResult ProcesarLogin(LoginViewModel model)
        {
            // Aqu� deber�as validar el modelo y autenticar al usuario
            if (ModelState.IsValid)
            {
                // Buscar el usuario por correo electr�nico en la base de datos
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == model.correo);

                if (usuario != null)
                {
                    // Validar la contrase�a
                    if (model.password == usuario.Contrasena)
                    {
                        // Autenticaci�n exitosa
                        return Json(new { success = true, message = "Inicio de sesi�n exitoso", redirect = Url.Action("Index", "Home") });
                    }
                    else
                    {
                        // Contrase�a incorrecta
                        return Json(new { error = "Credenciales incorrectas. Por favor, int�ntalo de nuevo." });
                    }
                }
                else
                {
                    // Usuario no encontrado
                    return Json(new { error = "No se encontr� ning�n usuario con el correo proporcionado." });
                }
            }
            else
            {
                // Modelo no v�lido
                return Json(new { error = "Por favor, completa todos los campos correctamente." });
            }

        }

        [HttpPost]
        public IActionResult ProcesarRegistro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aqu� podr�as guardar el nuevo usuario en la base de datos
                var nuevoUsuario = new Usuario
                {
                    Nombres = model.Nombres,
                    Apellidos = model.Apellidos,
                    Correo = model.Correo,
                    Contrasena = model.Contrasena,
                    Rolid = 2
                };

                _context.Usuarios.Add(nuevoUsuario);
                _context.SaveChanges();

                // Redirigir al usuario a la p�gina de inicio de sesi�n despu�s del registro
                string loginUrl = Url.Action("Login", "Home");
                return Json(new { success = true, message = "Registro exitoso", redirect = loginUrl });
            }
            else
            {
                // Modelo no v�lido, devolver errores de validaci�n
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { error = "Por favor, completa todos los campos correctamente.", errors = errors });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}