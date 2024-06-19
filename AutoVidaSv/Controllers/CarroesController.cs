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
    public class CarroesController : Controller
    {
        private readonly AutovidasvContext _context;

        public CarroesController(AutovidasvContext context)
        {
            _context = context;
        }

        // GET: Carroes
        public async Task<IActionResult> Index()
        {
            var autovidasvContext = _context.Carros.Include(c => c.Albumcar);
            return View(await autovidasvContext.ToListAsync());
        }

        // GET: Carroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Include(c => c.Albumcar)
                .FirstOrDefaultAsync(m => m.Autoid == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // GET: Carroes/Create
        public IActionResult Create()
        {
            ViewData["Albumcarid"] = new SelectList(_context.Albumcars, "Albumcarid", "Albumcarid");
            return View();
        }

        // POST: Carroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Autoid,Marca,Modelo,Anio,Transmicion,Combustible,Albumcarid")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Albumcarid"] = new SelectList(_context.Albumcars, "Albumcarid", "Albumcarid", carro.Albumcarid);
            return View(carro);
        }

        // GET: Carroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            ViewData["Albumcarid"] = new SelectList(_context.Albumcars, "Albumcarid", "Albumcarid", carro.Albumcarid);
            return View(carro);
        }

        // POST: Carroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Autoid,Marca,Modelo,Anio,Transmicion,Combustible,Albumcarid")] Carro carro)
        {
            if (id != carro.Autoid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.Autoid))
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
            ViewData["Albumcarid"] = new SelectList(_context.Albumcars, "Albumcarid", "Albumcarid", carro.Albumcarid);
            return View(carro);
        }

        // GET: Carroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Include(c => c.Albumcar)
                .FirstOrDefaultAsync(m => m.Autoid == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Carroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if (carro != null)
            {
                _context.Carros.Remove(carro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(int id)
        {
            return _context.Carros.Any(e => e.Autoid == id);
        }
    }
}
