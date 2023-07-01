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
    [Index(nameof(IdProductos), Name = "Productos_IdProductos_UQ", IsUnique = true)]
    [PrimaryKey(nameof(IdProductos))]
    public class Productos
    {
        public int IdProductos { get; set; }
        [Required(ErrorMessage = "El nombre del producto es Obligatorio")]
        [MaxLength(2, ErrorMessage = "Solo se aceptan hasta 65 caracteres en el nombre del producto")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage = "El precio del producto es Obligatorio")]
        [MaxLength(2, ErrorMessage = "Solo se aceptan hasta 2 decimales en el precio")]
        public int PrecioProducto { get; set; }
    }

}

