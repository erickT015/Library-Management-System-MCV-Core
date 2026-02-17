using AppCrudCore.Data;
using AppCrudCore.Models;
using AppCrudCore.Models.Enums;
using AppCrudCore.Models.ViewModels.TransaccionBiblioteca;
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

        private async Task CargarViewBag()
        {
            ViewBag.Clientes = new SelectList(
                await _context.Usuario
                    .Where(u => u.Activo && u.Rol.Nombre == "Cliente")
                    .ToListAsync(),
                "IdUsuario",
                "NombreCompleto"
            );

            ViewBag.Empleados = new SelectList(
                await _context.Usuario
                    .Where(u => u.Activo &&
                        (u.Rol.Nombre == "Empleado" || u.Rol.Nombre == "Admin"))
                    .ToListAsync(),
                "IdUsuario",
                "NombreCompleto"
            );
        }


        // GET: TransaccionBiblioteca
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.TransaccionBiblioteca
                .Include(t => t.ClienteUsuario)
        .Include(t => t.EmpleadoUsuario)
        .Include(t => t.Usuario)
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
                .Include(t => t.Usuario)
                //.Include(t => t.Empleado)
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
                    precioPrestamo= l.PrecioPrestamo,
                    stockPrestamo = l.StockPrestamo,
                    stockTotal = l.StockTotal,
                    text = $"{l.Titulo} | ISBN: {l.ISBN} | Sale: {l.StockVenta} | Rent: {l.StockPrestamo}"
                })
                .ToListAsync();

            return Json(libros);
        }


        //GET
        public IActionResult Create()
        {
            /*ViewBag.Cliente = new SelectList(
                _context.Cliente.Where(c => c.Activo),
                "IdCliente",
                "NombreCompleto"
            );

            ViewBag.Empleados = new SelectList(
                _context.Empleados.Where(e => e.Activo),
                "IdEmpleado",
                "NombreCompleto"
            );*/

            ViewBag.Clientes = new SelectList(
        _context.Usuario
            .Where(u => u.Activo && u.Rol.Nombre == "Cliente")
            .OrderBy(u => u.NombreCompleto)
            .ToList(),
        "IdUsuario",
        "NombreCompleto"
    );

            ViewBag.Empleados = new SelectList(
                _context.Usuario
                    .Where(u => u.Activo && (u.Rol.Nombre == "Empleado" || u.Rol.Nombre == "Admin"))
                    .OrderBy(u => u.NombreCompleto)
                    .ToList(),
                "IdUsuario",
                "NombreCompleto"
            );

            // CargarViewBag();
            return View();
        }


        //funcion para obtener el numero de transaccion
        private async Task<string> GenerarNumeroTransaccion()
        {
            var fecha = DateTime.Now;

            var ultimoId = await _context.TransaccionBiblioteca
                .OrderByDescending(t => t.IdTransaccionBiblioteca)
                .Select(t => t.IdTransaccionBiblioteca)
                .FirstOrDefaultAsync();

            var siguiente = ultimoId + 1;

            return $"TX-{fecha:yyyyMMdd}-{siguiente:D6}";
        }


        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransaccionBibliotecaCreateViewModel model)
        {
            await CargarViewBag();
            if (!ModelState.IsValid)
                return View(model);

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var transaccion = new TransaccionBiblioteca
                {
                    NumeroTransaccion = await GenerarNumeroTransaccion(),

                    ClienteUsuarioId = model.ClienteUsuarioId,

                    EmpleadoUsuarioId = model.EmpleadoUsuarioId,

                    // Usuario que creó el registro (temporal)
                    UsuarioId = model.EmpleadoUsuarioId,

                    TipoServicio = model.TipoServicio,
                    Origen = model.Origen,
                    Estado = model.TipoServicio == TipoServicio.Prestamo
                    ? EstadoTransaccion.Prestado
                    : EstadoTransaccion.Vendido,
                    TipoPago = model.TipoPago,

                    FechaCreacion = DateTime.Now,

                    FechaDevolucion = model.TipoServicio == TipoServicio.Prestamo
                        ? model.FechaDevolucion
                        : null,

                    ReferenciaPago = model.ReferenciaPago,
                    Observaciones = model.Observaciones
                };

                decimal total = 0;

                foreach (var item in model.Detalles)
                {
                    var libro = await _context.Libro.FindAsync(item.LibroId);

                    if (libro == null)
                        throw new Exception("Libro no existe");

                    decimal precioUnitario =
                        model.TipoServicio == TipoServicio.Prestamo
                        ? libro.PrecioPrestamo
                        : libro.PrecioVenta;

                    if (model.TipoServicio == TipoServicio.Venta)
                    {
                        if (libro.StockVenta < item.Cantidad)
                            throw new Exception($"Stock insuficiente: {libro.Titulo}");

                        libro.StockVenta -= item.Cantidad;
                        libro.StockTotal -= item.Cantidad;
                    }
                    else
                    {
                        if (libro.StockPrestamo < item.Cantidad)
                            throw new Exception($"Stock préstamo insuficiente: {libro.Titulo}");

                        libro.StockPrestamo -= item.Cantidad;
                    }

                    var subtotal = precioUnitario * item.Cantidad;

                    transaccion.Detalles.Add(new TransaccionDetalle
                    {
                        LibroId = item.LibroId,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = precioUnitario,
                        Subtotal = subtotal
                    });

                    total += subtotal;
                }

                transaccion.Total = total;
                transaccion.MontoPagado = model.MontoPagado;

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

                await CargarViewBag();
                return View(model);
            }
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var transaccion = await _context.TransaccionBiblioteca
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdTransaccionBiblioteca == id);

            if (transaccion == null)
                return NotFound();

            var model = new TransaccionBibliotecaEditViewModel
            {
                IdTransaccionBiblioteca = transaccion.IdTransaccionBiblioteca,
                Estado = transaccion.Estado,
                FechaDevolucion = transaccion.FechaDevolucion,
                Observaciones = transaccion.Observaciones
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TransaccionBibliotecaEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var transaccion = await _context.TransaccionBiblioteca
                .FirstOrDefaultAsync(x => x.IdTransaccionBiblioteca == model.IdTransaccionBiblioteca);

            if (transaccion == null)
                return NotFound();

            try
            {
                // reglas de negocio opcionales
                if (transaccion.Estado == EstadoTransaccion.Cancelado)
                {
                    ModelState.AddModelError("", "No se puede editar una transacción cancelada.");
                    return View(model);
                }

                transaccion.Estado = model.Estado;
                transaccion.FechaDevolucion = model.FechaDevolucion;
                transaccion.Observaciones = model.Observaciones;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        private bool TransaccionBibliotecaExists(int id)
        {
            return _context.TransaccionBiblioteca.Any(e => e.IdTransaccionBiblioteca == id);
        }
    }
}
