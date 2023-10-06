using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.BData.Data.Entity
{

    public class Venta
    {
        public int Id { get; set; }
        [Required]

        public int CodVenta { get; set; }
        [Required]
        public int CodProducto { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
		[Required]

		[Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
		public int Cantidad { get; set; }
    }
}
