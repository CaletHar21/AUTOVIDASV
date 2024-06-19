using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoVidaSv.Models;

namespace AutoVidaSv.Controllers
{
    public class AlbumcarsController : Controller
    {
        private readonly AutovidasvContext _context;

        public AlbumcarsController(AutovidasvContext context)
        {
            _context = context;
        }

        // GET: Albumcars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albumcars.ToListAsync());
        }

        // GET: Albumcars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumcar = await _context.Albumcars
                .FirstOrDefaultAsync(m => m.Albumcarid == id);
            if (albumcar == null)
            {
                return NotFound();
            }

            return View(albumcar);
        }

        // GET: Albumcars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albumcars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Albumcarid,Imagen")] Albumcar albumcar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(albumcar);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(albumcar);
        //}



        [HttpPost]
        public async Task<IActionResult> Create(IFormFile imagen)
        {
            if (imagen == null || imagen.Length == 0)
            {
                ModelState.AddModelError("Imagen", "Debe seleccionar una imagen.");
                return View();
            }

            if (!IsImageValid(imagen))
            {
                ModelState.AddModelError("Imagen", "El archivo seleccionado no es una imagen válida.");
                return View();
            }

            // Procesar la imagen aquí
            var albumcar = new Albumcar();

            // Guardar la imagen en el servidor (opcional)
            // var imagePath = await SaveImageAsync(imagen);

            // Ejemplo: Convertir imagen a base64 (opcional)
            var base64String = await ConvertToBase64Async(imagen);

            // Guardar la cadena base64 en el modelo
            albumcar.Imagen = base64String;

            // Guardar en la base de datos (ejemplo usando Entity Framework Core)
            _context.Albumcars.Add(albumcar);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool IsImageValid(IFormFile file)
        {
            if (file.ContentType.ToLower() != "image/jpg" &&
                file.ContentType.ToLower() != "image/jpeg" &&
                file.ContentType.ToLower() != "image/png" &&
                file.ContentType.ToLower() != "image/gif")
            {
                return false;
            }

            return true;
        }

        private async Task<string> ConvertToBase64Async(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }

        // GET: Albumcars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumcar = await _context.Albumcars.FindAsync(id);
            if (albumcar == null)
            {
                return NotFound();
            }
            return View(albumcar);
        }

        // POST: Albumcars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Albumcarid,Imagen")] Albumcar albumcar, IFormFile nuevaImagen)
        {
            if (id != albumcar.Albumcarid)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(albumcar);
            }

            try
            {
                if (nuevaImagen != null && nuevaImagen.Length > 0)
                {
                    if (!IsImageValid(nuevaImagen))
                    {
                        ModelState.AddModelError("Imagen", "El archivo seleccionado no es una imagen válida.");
                        return View(albumcar);
                    }

                    // Convertir la nueva imagen a base64 (opcional)
                    albumcar.Imagen = await ConvertToBase64Async(nuevaImagen);
                }

                _context.Update(albumcar);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumcarExists(albumcar.Albumcarid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Albumcars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumcar = await _context.Albumcars
                .FirstOrDefaultAsync(m => m.Albumcarid == id);
            if (albumcar == null)
            {
                return NotFound();
            }

            return View(albumcar);
        }

        // POST: Albumcars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albumcar = await _context.Albumcars.FindAsync(id);
            if (albumcar != null)
            {
                _context.Albumcars.Remove(albumcar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumcarExists(int id)
        {
            return _context.Albumcars.Any(e => e.Albumcarid == id);
        }
    }
}
