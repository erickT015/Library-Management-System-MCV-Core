namespace AppCrudCore.Models
{
    public class Rol
    {
        public static Rol? Empleado { get; internal set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

    }
}
