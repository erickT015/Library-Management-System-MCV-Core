using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCrudCore.Data;
using AppCrudCore.Models;

namespace AppCrudCore.Controllers
{
    public class TransaccionDetalleController : Controller
    {
        private readonly AppDBContext _context;

        public TransaccionDetalleController(AppDBContext context)
        {
            _context = context;
        }

        // GET: TransaccionDetalle
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.TransaccionDetalle.Include(t => t.Libro).Include(t => t.TransaccionBiblioteca);
            return View(await appDBContext.ToListAsync());
        }

        // GET: TransaccionDetalle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccionDetalle = await _context.TransaccionDetalle
                .Include(t => t.Libro)
                .Include(t => t.TransaccionBiblioteca)
                .FirstOrDefaultAsync(m => m.IdTransaccionDetalle == id);
            if (transaccionDetalle == null)
            {
                return NotFound();
            }

            return View(transaccionDetalle);
        }

        // GET: TransaccionDetalle/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libro, "IdLibro", "Autor");
            ViewData["TransaccionBibliotecaId"] = new SelectList(_context.TransaccionBiblioteca, "IdTransaccionBiblioteca", "IdTransaccionBiblioteca");
            return View();
        }

        // POST: TransaccionDetalle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransaccionDetalle,TransaccionBibliotecaId,LibroId,Cantidad,PrecioUnitario,Subtotal")] TransaccionDetalle transaccionDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccionDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libro, "IdLibro", "Autor", transaccionDetalle.LibroId);
            ViewData["TransaccionBibliotecaId"] = new SelectList(_context.TransaccionBiblioteca, "IdTransaccionBiblioteca", "IdTransaccionBiblioteca", transaccionDetalle.TransaccionBibliotecaId);
            return View(transaccionDetalle);
        }

        // GET: TransaccionDetalle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccionDetalle = await _context.TransaccionDetalle.FindAsync(id);
            if (transaccionDetalle == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libro, "IdLibro", "Autor", transaccionDetalle.LibroId);
            ViewData["TransaccionBibliotecaId"] = new SelectList(_context.TransaccionBiblioteca, "IdTransaccionBiblioteca", "IdTransaccionBiblioteca", transaccionDetalle.TransaccionBibliotecaId);
            return View(transaccionDetalle);
        }

        // POST: TransaccionDetalle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaccionDetalle,TransaccionBibliotecaId,LibroId,Cantidad,PrecioUnitario,Subtotal")] TransaccionDetalle transaccionDetalle)
        {
            if (id != transaccionDetalle.IdTransaccionDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccionDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionDetalleExists(transaccionDetalle.IdTransaccionDetalle))
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
            ViewData["LibroId"] = new SelectList(_context.Libro, "IdLibro", "Autor", transaccionDetalle.LibroId);
            ViewData["TransaccionBibliotecaId"] = new SelectList(_context.TransaccionBiblioteca, "IdTransaccionBiblioteca", "IdTransaccionBiblioteca", transaccionDetalle.TransaccionBibliotecaId);
            return View(transaccionDetalle);
        }

        // GET: TransaccionDetalle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccionDetalle = await _context.TransaccionDetalle
                .Include(t => t.Libro)
                .Include(t => t.TransaccionBiblioteca)
                .FirstOrDefaultAsync(m => m.IdTransaccionDetalle == id);
            if (transaccionDetalle == null)
            {
                return NotFound();
            }

            return View(transaccionDetalle);
        }

        // POST: TransaccionDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccionDetalle = await _context.TransaccionDetalle.FindAsync(id);
            if (transaccionDetalle != null)
            {
                _context.TransaccionDetalle.Remove(transaccionDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionDetalleExists(int id)
        {
            return _context.TransaccionDetalle.Any(e => e.IdTransaccionDetalle == id);
        }
    }
}
