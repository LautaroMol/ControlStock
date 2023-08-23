using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stock.BData.Data.Entity
{
    public class Producto
    {
        public int Id { get; set; }
        [Required]
        public int CodProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es Obligatorio")]
        [MaxLength(65, ErrorMessage = "Solo se aceptan hasta 65 caracteres en el nombre del producto")]
        public string NombreProducto { get; set; }
		[Required]
		public decimal PrecioProducto { get; set; }
		[Required]

		[Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
		public int Stock { get; set; }
    }

}

