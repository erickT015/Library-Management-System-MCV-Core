using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels.Profile
{
    public class ProfileEditViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        public string NombreCompleto { get; set; }


        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
        public string Telefono { get; set; }


        [StringLength(250, ErrorMessage = "la dirección no puede superar los 250 caracteres")]
        public string Direccion { get; set; }


        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string? NuevaPassword { get; set; }


        // Compare solo compara, no hace obligatorio el campo
        [Compare("NuevaPassword",
    ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmarPassword { get; set; }









    }
}
