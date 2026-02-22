using AppCrudCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"));
});

// Authentication con Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; //sino esta logeado , lo redirige a esta ruta
        options.AccessDeniedPath = "/Account/AccessDenied"; //esta logueado pero no tiene permisos, lo redirige a esta ruta
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true; //si esta activo, extiende la expiración cada vez que el usuario interactúa con la aplicación
        options.Cookie.HttpOnly = true;
    });

//  Authorization con Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("AdminOrEmpleado", policy =>
        policy.RequireRole("Admin", "Empleado"));

    options.AddPolicy("ClienteOnly", policy =>
        policy.RequireRole("Cliente"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();   // Primero identifica al usuario
app.UseAuthorization();    // Luego valida permisos
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libro}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();