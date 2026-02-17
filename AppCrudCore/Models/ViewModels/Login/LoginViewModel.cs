using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // opcional pero recomendado
        public bool Recordarme { get; set; }
    }
}
