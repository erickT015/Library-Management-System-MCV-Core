using AppCrudCore.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels.TransaccionBiblioteca
{
    public class TransaccionBibliotecaEditViewModel
    {
        public int IdTransaccionBiblioteca { get; set; }

        [Required]
        public EstadoTransaccion Estado { get; set; }

        public DateTime? FechaDevolucion { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }
    }
}
