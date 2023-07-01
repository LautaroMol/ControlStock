using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.BData.Data.Entity
{
    [Index(nameof(IdProd), Name = "Informe_IdProd_UQ", IsUnique = true)]
    [PrimaryKey(nameof(IdProd))]
    public class Informe
    {
        public int IdProd { get; set; } 
        public int Stock { get; set; }
        public required Productos IdProducto { get; set; }


    }
}
