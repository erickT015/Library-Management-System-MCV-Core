using AppCrudCore.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppCrudCore.Models.ViewModels
{
    public class TransaccionBibliotecaCreateViewModel
    {
    [Required]
        public int ClienteId { get; set; }

        public int? EmpleadoId { get; set; }

        [Required]
        public TipoServicio TipoServicio { get; set; }

        [Required]
        public OrigenTransaccion Origen { get; set; }

        public List<CreateTransaccionDetalleViewModel> Detalles { get; set; }
            = new List<CreateTransaccionDetalleViewModel>();
    }


    public class CreateTransaccionDetalleViewModel
    {
        [Required]
        public int LibroId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
    }

}
