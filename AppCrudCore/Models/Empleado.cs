namespace AppCrudCore.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }

        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }

        public string PasswordHash { get; set; }
        public DateOnly FechaContrato { get; set; }
        public bool Activo { get; set; }

        public int RolId { get; set; } //FK Rol

        public virtual Rol? Rol { get; set; } //propiedad de navegación

    }
}