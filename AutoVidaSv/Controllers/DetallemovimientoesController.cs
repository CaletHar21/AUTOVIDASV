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
    public class DetallemovimientoesController : Controller
    {
        private readonly AutovidasvContext _context;

        public DetallemovimientoesController(AutovidasvContext context)
        {
            _context = context;
        }

        // GET: Detallemovimientoes
        public async Task<IActionResult> Index()
        {
            var autovidasvContext = _context.Detallemovimientos.Include(d => d.Auto).Include(d => d.Renta).Include(d => d.Venta);
            return View(await autovidasvContext.ToListAsync());
        }

        // GET: Detallemovimientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallemovimiento = await _context.Detallemovimientos
                .Include(d => d.Auto)
                .Include(d => d.Renta)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Detallemovimientoid == id);
            if (detallemovimiento == null)
            {
                return NotFound();
            }

            return View(detallemovimiento);
        }

        // GET: Detallemovimientoes/Create
        public IActionResult Create()
        {
            ViewData["Autoid"] = new SelectList(_context.Carros, "Autoid", "Autoid");
            ViewData["Rentaid"] = new SelectList(_context.Rentas, "Rentaid", "Rentaid");
            ViewData["Ventaid"] = new SelectList(_context.Ventas, "Ventaid", "Ventaid");
            return View();
        }

        // POST: Detallemovimientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Detallemovimientoid,Autoid,Rentaid,Ventaid,Fecha,Usuarioid,Facturacionid")] Detallemovimiento detallemovimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallemovimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Autoid"] = new SelectList(_context.Carros, "Autoid", "Autoid", detallemovimiento.Autoid);
            ViewData["Rentaid"] = new SelectList(_context.Rentas, "Rentaid", "Rentaid", detallemovimiento.Rentaid);
            ViewData["Ventaid"] = new SelectList(_context.Ventas, "Ventaid", "Ventaid", detallemovimiento.Ventaid);
            return View(detallemovimiento);
        }

        // GET: Detallemovimientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallemovimiento = await _context.Detallemovimientos.FindAsync(id);
            if (detallemovimiento == null)
            {
                return NotFound();
            }
            ViewData["Autoid"] = new SelectList(_context.Carros, "Autoid", "Autoid", detallemovimiento.Autoid);
            ViewData["Rentaid"] = new SelectList(_context.Rentas, "Rentaid", "Rentaid", detallemovimiento.Rentaid);
            ViewData["Ventaid"] = new SelectList(_context.Ventas, "Ventaid", "Ventaid", detallemovimiento.Ventaid);
            return View(detallemovimiento);
        }

        // POST: Detallemovimientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Detallemovimientoid,Autoid,Rentaid,Ventaid,Fecha,Usuarioid,Facturacionid")] Detallemovimiento detallemovimiento)
        {
            if (id != detallemovimiento.Detallemovimientoid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallemovimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallemovimientoExists(detallemovimiento.Detallemovimientoid))
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
            ViewData["Autoid"] = new SelectList(_context.Carros, "Autoid", "Autoid", detallemovimiento.Autoid);
            ViewData["Rentaid"] = new SelectList(_context.Rentas, "Rentaid", "Rentaid", detallemovimiento.Rentaid);
            ViewData["Ventaid"] = new SelectList(_context.Ventas, "Ventaid", "Ventaid", detallemovimiento.Ventaid);
            return View(detallemovimiento);
        }

        // GET: Detallemovimientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallemovimiento = await _context.Detallemovimientos
                .Include(d => d.Auto)
                .Include(d => d.Renta)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Detallemovimientoid == id);
            if (detallemovimiento == null)
            {
                return NotFound();
            }

            return View(detallemovimiento);
        }

        // POST: Detallemovimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallemovimiento = await _context.Detallemovimientos.FindAsync(id);
            if (detallemovimiento != null)
            {
                _context.Detallemovimientos.Remove(detallemovimiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallemovimientoExists(int id)
        {
            return _context.Detallemovimientos.Any(e => e.Detallemovimientoid == id);
        }
    }
}
