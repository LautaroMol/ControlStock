using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStock.Shared.DTO
{
    public class ProductoDTO
    {
        [Required]
        public int CodProducto { get; set; }
        public string NombreProducto { get; set; }
        [Required]

        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int PrecioProducto { get; set; }
        [Required]

        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int Stock { get; set; }
    }
}
