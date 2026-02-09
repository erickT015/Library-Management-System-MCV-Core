using AppCrudCore.Data;
using AppCrudCore.Models;
using AppCrudCore.Models.Enums;
using AppCrudCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var appDBContext = _context.TransaccionBiblioteca
                .Include(t => t.Cliente)
                .Include(t => t.Empleado)
                .Include(t => t.Detalles)
                .ThenInclude(d => d.Libro);
            return View(await appDBContext.ToListAsync());
        }

        // GET: TransaccionBiblioteca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccionBiblioteca = await _context.TransaccionBiblioteca
                .Include(t => t.Cliente)
                .Include(t => t.Empleado)
                .FirstOrDefaultAsync(m => m.IdTransaccionBiblioteca == id);
            if (transaccionBiblioteca == null)
            {
                return NotFound();
            }

            return View(transaccionBiblioteca);
        }


        // GET: Funcion de busqueda por debounce
        [HttpGet]
        public async Task<IActionResult> BuscarLibros(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            var libros = await _context.Libro
                .Where(libro => libro.Activo &&
                       (libro.Titulo.Contains(term) || libro.ISBN.Contains(term)))
                .OrderBy(libro => libro.Titulo)
                .Take(20)
                .Select(l => new
                {
                    id = l.IdLibro,
                    titulo = l.Titulo,
                    isbn = l.ISBN,
                    precio = l.PrecioVenta,
                    stockVenta = l.StockVenta,
                    stockPrestamo = l.StockPrestamo,
                    stockTotal = l.StockTotal,
                    text = $"{l.Titulo} | ISBN: {l.ISBN} | Venta: {l.StockVenta} | Préstamo: {l.StockPrestamo}"
                })
                .ToListAsync();

            return Json(libros);
        }


        //GET
        public IActionResult Create()
        {
            ViewBag.Cliente = new SelectList(
                _context.Cliente.Where(c => c.Activo),
                "IdCliente",
                "NombreCompleto"
            );

            ViewBag.Empleados = new SelectList(
                _context.Empleados.Where(e => e.Activo),
                "IdEmpleado",
                "NombreCompleto"
            );

            return View();
        }


        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransaccionBibliotecaCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var transaccion = new TransaccionBiblioteca
                {
                    ClienteId = model.ClienteId,
                    EmpleadoId = model.EmpleadoId,
                    TipoServicio = model.TipoServicio,
                    Origen = model.Origen,
                    Estado = EstadoTransaccion.Completada,
                    FechaCreacion = DateTime.Now
                };

                decimal total = 0;

                foreach (var item in model.Detalles)
                {
                    var libro = await _context.Libro.FindAsync(item.LibroId);

                    if (libro == null)
                        throw new Exception("Libro no encontrado");

                    if (model.TipoServicio == TipoServicio.Venta)
                    {
                        if (libro.StockVenta < item.Cantidad)
                            throw new Exception($"Stock de venta insuficiente para {libro.Titulo}");

                        libro.StockVenta -= item.Cantidad;
                    }
                    else
                    {
                        if (libro.StockPrestamo < item.Cantidad)
                            throw new Exception($"Stock de préstamo insuficiente para {libro.Titulo}");

                        libro.StockPrestamo -= item.Cantidad;
                    }

                    libro.StockTotal -= item.Cantidad;

                    var subtotal = libro.PrecioVenta * item.Cantidad;

                    var detalle = new TransaccionDetalle
                    {
                        LibroId = libro.IdLibro,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = libro.PrecioVenta,
                        Subtotal = subtotal
                    };

                    total += subtotal;

                    transaccion.Detalles.Add(detalle);
                }

                transaccion.Total = total;

                 _context.TransaccionBiblioteca.Add(transaccion);

                await _context.SaveChangesAsync();

                await dbTransaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                var error = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError("", error);

                return View(model);
            }
        }

        private bool TransaccionBibliotecaExists(int id)
        {
            return _context.TransaccionBiblioteca.Any(e => e.IdTransaccionBiblioteca == id);
        }
    }
}
