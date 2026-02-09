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
    public class TransaccionBibliotecaController : Controller
    {
        private readonly AppDBContext _context;

        public TransaccionBibliotecaController(AppDBContext context)
        {
            _context = context;
        }

        // GET: TransaccionBiblioteca
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.TransaccionesBiblioteca.Include(t => t.Cliente).Include(t => t.Empleado);
            return View(await appDBContext.ToListAsync());
        }

        // GET: TransaccionBiblioteca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccionBiblioteca = await _context.TransaccionesBiblioteca
                .Include(t => t.Cliente)
                .Include(t => t.Empleado)
                .FirstOrDefaultAsync(m => m.IdTransaccionBiblioteca == id);
            if (transaccionBiblioteca == null)
            {
                return NotFound();
            }

            return View(transaccionBiblioteca);
        }

        // GET: TransaccionBiblioteca/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "IdCliente", "Cedula");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "IdEmpleado", "Cedula");
            return View();
        }

        // POST: TransaccionBiblioteca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransaccionBiblioteca,ClienteId,EmpleadoId,TipoServicio,Origen,Estado,Total,FechaCreacion,FechaCompletada")] TransaccionBiblioteca transaccionBiblioteca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccionBiblioteca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "IdCliente", "Cedula", transaccionBiblioteca.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "IdEmpleado", "Cedula", transaccionBiblioteca.EmpleadoId);
            return View(transaccionBiblioteca);
        }

        // GET: TransaccionBiblioteca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccionBiblioteca = await _context.TransaccionesBiblioteca.FindAsync(id);
            if (transaccionBiblioteca == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "IdCliente", "Cedula", transaccionBiblioteca.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "IdEmpleado", "Cedula", transaccionBiblioteca.EmpleadoId);
            return View(transaccionBiblioteca);
        }

        // POST: TransaccionBiblioteca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaccionBiblioteca,ClienteId,EmpleadoId,TipoServicio,Origen,Estado,Total,FechaCreacion,FechaCompletada")] TransaccionBiblioteca transaccionBiblioteca)
        {
            if (id != transaccionBiblioteca.IdTransaccionBiblioteca)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccionBiblioteca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionBibliotecaExists(transaccionBiblioteca.IdTransaccionBiblioteca))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "IdCliente", "Cedula", transaccionBiblioteca.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "IdEmpleado", "Cedula", transaccionBiblioteca.EmpleadoId);
            return View(transaccionBiblioteca);
        }

        // GET: TransaccionBiblioteca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccionBiblioteca = await _context.TransaccionesBiblioteca
                .Include(t => t.Cliente)
                .Include(t => t.Empleado)
                .FirstOrDefaultAsync(m => m.IdTransaccionBiblioteca == id);
            if (transaccionBiblioteca == null)
            {
                return NotFound();
            }

            return View(transaccionBiblioteca);
        }

        // POST: TransaccionBiblioteca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccionBiblioteca = await _context.TransaccionesBiblioteca.FindAsync(id);
            if (transaccionBiblioteca != null)
            {
                _context.TransaccionesBiblioteca.Remove(transaccionBiblioteca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionBibliotecaExists(int id)
        {
            return _context.TransaccionesBiblioteca.Any(e => e.IdTransaccionBiblioteca == id);
        }
    }
}
