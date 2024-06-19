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
    public class RentasController : Controller
    {
        private readonly AutovidasvContext _context;

        public RentasController(AutovidasvContext context)
        {
            _context = context;
        }

        // GET: Rentas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rentas.ToListAsync());
        }

        // GET: Rentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renta = await _context.Rentas
                .FirstOrDefaultAsync(m => m.Rentaid == id);
            if (renta == null)
            {
                return NotFound();
            }

            return View(renta);
        }

        // GET: Rentas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rentaid,Fechaentrega,Fechadevolucion,Totaldedias,Agregardiasextra,Totalacancelar")] Renta renta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(renta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(renta);
        }

        // GET: Rentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renta = await _context.Rentas.FindAsync(id);
            if (renta == null)
            {
                return NotFound();
            }
            return View(renta);
        }

        // POST: Rentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rentaid,Fechaentrega,Fechadevolucion,Totaldedias,Agregardiasextra,Totalacancelar")] Renta renta)
        {
            if (id != renta.Rentaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(renta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentaExists(renta.Rentaid))
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
            return View(renta);
        }

        // GET: Rentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renta = await _context.Rentas
                .FirstOrDefaultAsync(m => m.Rentaid == id);
            if (renta == null)
            {
                return NotFound();
            }

            return View(renta);
        }

        // POST: Rentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var renta = await _context.Rentas.FindAsync(id);
            if (renta != null)
            {
                _context.Rentas.Remove(renta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentaExists(int id)
        {
            return _context.Rentas.Any(e => e.Rentaid == id);
        }
    }
}
