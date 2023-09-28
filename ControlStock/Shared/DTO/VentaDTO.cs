using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStock.Shared.DTO
{
    public class VentaDTO
    {
        [Required]
        public int CodVenta { get; set; }
        public string Producto { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
        [Required]

        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido y debe ser mayor a 0")]
        public int Cantidad { get; set; }
        [Required]
        public int CodProducto { get; set; }
        [Required]
        public ProductoDTO productoDTO { get; set; }
    }
}
