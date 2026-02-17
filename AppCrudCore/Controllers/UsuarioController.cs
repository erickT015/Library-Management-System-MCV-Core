using AppCrudCore.Data;
using AppCrudCore.Models;
using AppCrudCore.Models.ViewModels.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCrudCore.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDBContext _context;

        public UsuarioController(AppDBContext context)
        {
            _context = context;
        }

        private void CargarRoles()//METODO PRIVADO PARA CARGAR ROLES
        {
            ViewBag.RolId = _context.Rol
        .Where(r => r.Activo)
        .Select(r => new SelectListItem
        {
            Value = r.IdRol.ToString(),
            Text = r.Nombre
        })
        .ToList();
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            var usuario =await  _context.Usuario
                .Include(u => u.Rol) //conectar con tabla rol
                .AsNoTracking()     //solo lectura
                .Where(u => u.Activo) // solo activos
                .ToListAsync();     //enlistar items

            return View(usuario);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            CargarRoles();
            //ViewData["RolId"] = new SelectList(_context.Rol, "IdRol", "Nombre");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCreateViewModel vm)
        {
            // vm contiene el Password en texto plano, NO el hash
            try
            {
                CargarRoles();
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                // Crear nueva entidad Usuario
                var NuevoUsuario = new Usuario
                {
                    Correo = vm.Correo,// asigna correo
                    Cedula = vm.Cedula,// asigna cédula
                    NombreCompleto = vm.NombreCompleto,// asigna nombre
                    Telefono = vm.Telefono,// asigna teléfono
                    Direccion = vm.Direccion,// asigna dirección
                    RolId = vm.RolId,// asigna rol
                    Activo = true,// asigna estado activo
                    FechaRegistro = DateTime.Now,// se asigna en servidor, nunca desde la vista
                    UltimoLogin = null,// aún no ha iniciado sesión
                    RequiereCambioPassword = false,// fuerza cambio en primer login
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password)// aquí se convierte el password plano a hash seguro
                };

                await _context.Usuario.AddAsync(NuevoUsuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Ah ocurrido este error" + ex
                );
                return View(vm);
            }
        }


        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var usuarioDb = await _context.Usuario
                .FirstOrDefaultAsync(e => e.IdUsuario == id);

            if (usuarioDb == null)
                return NotFound();

            var vm = new UsuarioEditViewModel
            {
                IdUsuario = usuarioDb.IdUsuario,
                Correo = usuarioDb.Correo,
                Cedula = usuarioDb.Cedula,
                NombreCompleto = usuarioDb.NombreCompleto,
                Telefono = usuarioDb.Telefono,
                Direccion = usuarioDb.Direccion,
                Activo = usuarioDb.Activo,
                RolId = usuarioDb.RolId
            };

            CargarRoles();

            return View(vm);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioEditViewModel vm)
        {
            //	$2a$11$R6f/LrCY/qUGJQpTSBADpeyIdUiH9eFP0FnCx9cXPAYHVVdgG9Mfq
            //  $2a$11$qxFeny1Ns8BZRLlX4Ar8IuqbS/XotH1BG6tfWgNzUGHqZ.wKe/pD.
            try
            {
                if (!ModelState.IsValid)
                {
                    CargarRoles();
                    return View(vm);
                }

                var usuarioDb = await _context.Usuario
                    .FirstOrDefaultAsync(u => u.IdUsuario == vm.IdUsuario);

                if (usuarioDb == null)
                    return NotFound();

                // actualizar campos editables
                usuarioDb.Correo = vm.Correo;
                usuarioDb.Cedula = vm.Cedula;
                usuarioDb.NombreCompleto = vm.NombreCompleto;
                usuarioDb.Telefono = vm.Telefono;
                usuarioDb.Direccion = vm.Direccion;
                usuarioDb.Activo = vm.Activo;
                usuarioDb.RolId = vm.RolId;

                // actualizar password solo si se ingresó una nueva
                if (!string.IsNullOrWhiteSpace(vm.NuevaPassword))
                {
                    if (vm.NuevaPassword.Length < 8)
                    {
                        ModelState.AddModelError("NuevaPassword", "Debe tener al menos 8 caracteres");
                        CargarRoles();
                        return View(vm);
                    }

                    if (vm.NuevaPassword != vm.ConfirmarPassword)
                    {
                        ModelState.AddModelError("ConfirmarPassword", "Las contraseñas no coinciden");
                        CargarRoles();
                        return View(vm);
                    }

                    usuarioDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.NuevaPassword);

                    usuarioDb.RequiereCambioPassword = false;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Ah ocurrido este error" + ex
                );
                CargarRoles();
                return View(vm);
            }
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuarioDb = await _context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuarioDb == null)
                return NotFound();

            var vm = new UsuarioDeleteViewModel
            {
                IdUsuario = usuarioDb.IdUsuario,
                Correo = usuarioDb.Correo,
                Cedula = usuarioDb.Cedula,
                NombreCompleto = usuarioDb.NombreCompleto,
                Telefono = usuarioDb.Telefono,
                Direccion = usuarioDb.Direccion,
                Activo = usuarioDb.Activo,
                NombreRol = usuarioDb.Rol?.Nombre
            };

            return View(vm);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario != null)
            {
                usuario.Activo = false;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.IdUsuario == id);
        }
    }
}
