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
		[Range(1, int.MaxValue, ErrorMessage = "El codigo del producto es requerido")]
		public int CodProducto { get; set; }
		[Required(ErrorMessage = "El nombre del producto es Obligatorio")]
		[MaxLength(65, ErrorMessage = "Solo se aceptan hasta 65 caracteres en el nombre del producto")]
		public string NombreProducto { get; set; }
        [Required]
		[Range(1, int.MaxValue, ErrorMessage = "El precio del producto es requerido")]
		public decimal PrecioProducto { get; set; }
        [Required]

        [Range(1, int.MaxValue, ErrorMessage = "El stock del producto es requerido")]
        public int Stock { get; set; }
    }
}
