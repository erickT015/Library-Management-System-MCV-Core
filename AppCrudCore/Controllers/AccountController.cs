using AppCrudCore.Data;
using AppCrudCore.Models.ViewModels.Login;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppCrudCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;

        public AccountController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == model.Correo);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos");
                return View(model);
            }

            bool passwordValido = BCrypt.Net.BCrypt.Verify(model.Password, usuario.PasswordHash);

            if (!passwordValido)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos");
                return View(model);
            }

            // AQUI luego crearemos las Claims

            return RedirectToAction("Index", "Home");
        }

        // POST: ForgotPassword
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login");

            // lógica futura
            return RedirectToAction("Login");
        }
    }
}
