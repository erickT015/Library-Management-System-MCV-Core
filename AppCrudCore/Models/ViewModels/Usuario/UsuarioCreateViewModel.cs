using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels.Usuario
{
    public class UsuarioCreateViewModel
    {
        //public int IdUsuario { get; set; }

        //IDENTIDAD
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres")]
        public string Correo { get; set; }


        [Required]
        public String Password { get; set; } // el quivalente a PasswordHash

        //CONTACTO
        [Required(ErrorMessage = "la cédula es obligatorio")]
        [StringLength(20, ErrorMessage = "La cédula no puede superar los 20 caracteres")]
        public string Cedula { get; set; }


        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        public string NombreCompleto { get; set; }


        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
        public string Telefono { get; set; }


        [StringLength(250, ErrorMessage = "la dirección no puede superar los 250 caracteres")]
        public string Direccion { get; set; }

        //SEGURIDAD
        //[Required(ErrorMessage = "La fecha de registro es obligatorio")]
        //public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? UltimoLogin { get; set; }

        public bool RequiereCambioPassword { get; set; } = false;


        [Required]
        public bool Activo { get; set; }

        public int? RolId { get; set; } //FK Rol
    }
}
