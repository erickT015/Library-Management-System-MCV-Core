namespace AppCrudCore.Models.ViewModels.Usuario
{
    public class UsuarioDeleteViewModel
    {
        public int IdUsuario { get; set; }

        public string Correo { get; set; }

        public string Cedula { get; set; }

        public string NombreCompleto { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public DateTime? UltimoLogin { get; set; }

        public string? NombreRol { get; set; }
        public Rol? Rol { get; set; }     // navegación (lectura)
    }
}
