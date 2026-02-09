using AppCrudCore.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models
{
    public class TransaccionBiblioteca : IValidatableObject
    {
        public int IdTransaccionBiblioteca { get; set; }

        [Required]
        public int ClienteId { get; set; } //FK
        public Cliente Cliente { get; set; }

        public int? EmpleadoId { get; set; } //FK
        public Empleado? Empleado { get; set; }


        [Required]
        public TipoServicio TipoServicio { get; set; }


        [Required]
        public OrigenTransaccion Origen { get; set; }


        [Required]
        public EstadoTransaccion Estado { get; set; }


        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Total debe ser mayor que 0.")]
        public decimal Total { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaCompletada { get; set; }

        public ICollection<TransaccionDetalle> Detalles { get; set; } = new List<TransaccionDetalle>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Detalles != null && Detalles.Any())
            {
                var sumaDetalles = Detalles.Sum(d => d.Subtotal);

                if (Total != sumaDetalles)
                {
                    yield return new ValidationResult(
                        "Total debe coincidir con la suma de los Subtotales.",
                        new[] { nameof(Total) }
                    );
                }
            }
        }
    }
}
