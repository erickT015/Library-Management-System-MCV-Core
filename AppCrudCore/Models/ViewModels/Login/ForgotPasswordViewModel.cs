using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels.Login
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; }
    }
}
