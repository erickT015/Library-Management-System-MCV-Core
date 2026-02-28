using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models
{
    public class Libro : IValidatableObject
    {

        public int IdLibro { get; set; }

        [StringLength(300)]
        [Display(Name = "Imagen")]
        public string? ImagenUrl { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede superar los 200 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(150, ErrorMessage = "El autor no puede superar los 150 caracteres")]
        public string Autor { get; set; }


        [Required(ErrorMessage = "El ISBN es obligatorio")]
        public string ISBN {  get; set; }


        [Required(ErrorMessage = "El año de publicación es obligatorio")]
        public int AnioPublicacion { get; set; }


        [Required(ErrorMessage = "El resumen es obligatorio")]
        [StringLength(500, ErrorMessage = "El resumen no puede superar los 500 caracteres")]
        public string Resumen {  get; set; }


        [Required(ErrorMessage = "El precio de venta es obligatorio")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
        public decimal PrecioVenta { get; set; }


        [Required(ErrorMessage = "El precio de préstamo es obligatorio")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
        public decimal PrecioPrestamo { get; set; }


        [Required(ErrorMessage = "La cantidad total es obligatoria")]
        [Range(0,int.MaxValue)] //minimo 0, no negativos]
        public int StockTotal { get; set; }


        [Range(0, int.MaxValue)]
        public int StockPrestamo { get; set; }


        [Range(0, int.MaxValue)]
        public int StockVenta { get; set; }


        [Required]
        public bool Activo {  get; set; }


        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int CategoriaId { get; set; } //FK
        public Categoria? Categoria { get; set; }   // navegación (lectura)

        // ===============================
        // REGLAS DE NEGOCIO
        // ===============================

        public void ReducirStockVenta(int cantidad)
        {
            if (cantidad <= 0)
                throw new InvalidOperationException(
                    "Cantidad inválida.");

            if (StockVenta < cantidad)
                throw new InvalidOperationException(
                    $"Stock insuficiente para venta: {Titulo}");

            StockVenta -= cantidad;
            StockTotal -= cantidad;

            ActualizarEstadoActivo();
        }

        public void ReducirStockPrestamo(int cantidad)
        {
            if (cantidad <= 0)
                throw new InvalidOperationException(
                    "Cantidad inválida.");

            if (StockPrestamo < cantidad)
                throw new InvalidOperationException(
                    $"Stock insuficiente para préstamo: {Titulo}");

            StockPrestamo -= cantidad;

            ActualizarEstadoActivo();
        }

        private void ActualizarEstadoActivo()
        {
            Activo = StockVenta > 0 || StockPrestamo > 0;
        }

        public void DevolverPrestamo(int cantidad)
        {
            if (cantidad <= 0)
                throw new InvalidOperationException(
                    "Cantidad inválida.");

            StockPrestamo += cantidad;

            ActualizarEstadoActivo();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //ejecutar siempre que se actualice
            ActualizarEstadoActivo();

            if (StockPrestamo + StockVenta > StockTotal)
            {
                yield return new ValidationResult(
                    "Stock Prestamo + Stock Venta no puede ser mayor que StockTotal.",
                    new[] { nameof(StockPrestamo), nameof(StockVenta), nameof(StockTotal) }
                );
            }

            if (AnioPublicacion > DateTime.Now.Year)
            {
                yield return new ValidationResult(
                    $"El año no puede ser mayor que {DateTime.Now.Year}.",
                    new[] { nameof(AnioPublicacion) }
                );
            }
        }


    }
}
