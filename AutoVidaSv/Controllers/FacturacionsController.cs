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
    public class FacturacionsController : Controller
    {
        private readonly AutovidasvContext _context;

        public FacturacionsController(AutovidasvContext context)
        {
            _context = context;
        }

        // GET: Facturacions
        public async Task<IActionResult> Index()
        {
            var autovidasvContext = _context.Facturacions.Include(f => f.Detallemovimiento).Include(f => f.Usuairo);
            return View(await autovidasvContext.ToListAsync());
        }

        // GET: Facturacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacions
                .Include(f => f.Detallemovimiento)
                .Include(f => f.Usuairo)
                .FirstOrDefaultAsync(m => m.Facturacionid == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // GET: Facturacions/Create
        public IActionResult Create()
        {
            ViewData["Detallemovimientoid"] = new SelectList(_context.Detallemovimientos, "Detallemovimientoid", "Detallemovimientoid");
            ViewData["Usuairoid"] = new SelectList(_context.Usuarios, "Usuarioid", "Usuarioid");
            return View();
        }

        // POST: Facturacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Facturacionid,Fecha,Usuairoid,Detallemovimientoid")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Detallemovimientoid"] = new SelectList(_context.Detallemovimientos, "Detallemovimientoid", "Detallemovimientoid", facturacion.Detallemovimientoid);
            ViewData["Usuairoid"] = new SelectList(_context.Usuarios, "Usuarioid", "Usuarioid", facturacion.Usuairoid);
            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacions.FindAsync(id);
            if (facturacion == null)
            {
                return NotFound();
            }
            ViewData["Detallemovimientoid"] = new SelectList(_context.Detallemovimientos, "Detallemovimientoid", "Detallemovimientoid", facturacion.Detallemovimientoid);
            ViewData["Usuairoid"] = new SelectList(_context.Usuarios, "Usuarioid", "Usuarioid", facturacion.Usuairoid);
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Facturacionid,Fecha,Usuairoid,Detallemovimientoid")] Facturacion facturacion)
        {
            if (id != facturacion.Facturacionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturacionExists(facturacion.Facturacionid))
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
            ViewData["Detallemovimientoid"] = new SelectList(_context.Detallemovimientos, "Detallemovimientoid", "Detallemovimientoid", facturacion.Detallemovimientoid);
            ViewData["Usuairoid"] = new SelectList(_context.Usuarios, "Usuarioid", "Usuarioid", facturacion.Usuairoid);
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacions
                .Include(f => f.Detallemovimiento)
                .Include(f => f.Usuairo)
                .FirstOrDefaultAsync(m => m.Facturacionid == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturacion = await _context.Facturacions.FindAsync(id);
            if (facturacion != null)
            {
                _context.Facturacions.Remove(facturacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturacionExists(int id)
        {
            return _context.Facturacions.Any(e => e.Facturacionid == id);
        }
    }
}
