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
            // Aquí deberías validar el modelo y autenticar al usuario
            if (ModelState.IsValid)
            {
                // Buscar el usuario por correo electrónico en la base de datos
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == model.correo);

                if (usuario != null)
                {
                    // Validar la contraseña
                    if (model.password == usuario.Contrasena)
                    {
                        // Autenticación exitosa
                        return Json(new { success = true, message = "Inicio de sesión exitoso", redirect = Url.Action("Index", "Home") });
                    }
                    else
                    {
                        // Contraseña incorrecta
                        return Json(new { error = "Credenciales incorrectas. Por favor, inténtalo de nuevo." });
                    }
                }
                else
                {
                    // Usuario no encontrado
                    return Json(new { error = "No se encontró ningún usuario con el correo proporcionado." });
                }
            }
            else
            {
                // Modelo no válido
                return Json(new { error = "Por favor, completa todos los campos correctamente." });
            }

        }

        [HttpPost]
        public IActionResult ProcesarRegistro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aquí podrías guardar el nuevo usuario en la base de datos
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

                // Redirigir al usuario a la página de inicio de sesión después del registro
                string loginUrl = Url.Action("Login", "Home");
                return Json(new { success = true, message = "Registro exitoso", redirect = loginUrl });
            }
            else
            {
                // Modelo no válido, devolver errores de validación
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
