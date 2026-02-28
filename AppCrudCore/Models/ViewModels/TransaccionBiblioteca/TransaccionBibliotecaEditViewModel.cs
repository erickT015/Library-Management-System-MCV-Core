using AppCrudCore.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels.TransaccionBiblioteca
{
    public class TransaccionBibliotecaEditViewModel
    {
        public int IdTransaccionBiblioteca { get; set; }

        [Required]
        public EstadoTransaccion Estado { get; set; }

        public DateTime? FechaDevolucion { get; set; }

        [BindNever]
        public DateTime? FechaCreacion { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }


        [BindNever]
        public ICollection<TransaccionDetalle> Detalles { get; set; }
            = new List<TransaccionDetalle>();
    }
}
