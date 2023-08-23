using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.BData.Data.Entity
{
    [Index(nameof(IdVenta), Name = "Informe_IdProd_UQ", IsUnique = true)]
    [PrimaryKey(nameof(IdVenta))]
    public class Venta
    {
        public int Id { get; set; }
        [Required]

        public int CodVenta { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Solo se aceptan hasta 50 caracteres en el nombre del producto")]
        public string Producto { get; set; }
        [Required]
        public int CodProd { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int Precio { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
		[Required]

		[Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
		public int Cantidad { get; set; }
    }
}
