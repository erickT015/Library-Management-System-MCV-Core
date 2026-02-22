using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres")]
        public string Correo { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "la cédula es obligatorio")]
        [StringLength(20, ErrorMessage = "La cédula no puede superar los 20 caracteres")]
        public string Cedula { get; set; }


        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        public string NombreCompleto { get; set; }


        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
        public string? Telefono { get; set; }


        [StringLength(250, ErrorMessage = "la dirección no puede superar los 250 caracteres")]
        public string? Direccion { get; set; }
    }
}
