using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Stock.BData;
using Stock.BData.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ControlStock.Shared.DTO;
using BlazorCrud.Shared;

namespace ControlStock.Server.Controllers
{
    [ApiController]
    [Route("api/Producto")]
    public class ProductoController : ControllerBase
    {
        private readonly Context context;

        public ProductoController(Context dbcontext)
        {
            context = dbcontext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            var productos = await context.Productos.ToListAsync();
            return productos;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int Id)
        {
            var buscar = await context.Productos.FirstOrDefaultAsync(c => c.Id== Id);
            if (buscar is null)
            {
                return BadRequest($"Producto no encontrado, verifique el id: {Id}");
            }
            return Ok(buscar);
        }

        [HttpPost]

        public async Task<IActionResult> Post(ProductoDTO productoDTO)
        {
            try
            {
                var exist = await context.Productos.FirstOrDefaultAsync(e => e.CodProducto == productoDTO.CodProducto);
				if (exist is null) {
					var mdProducto = new Producto
					{
						CodProducto = productoDTO.CodProducto,
						NombreProducto = productoDTO.NombreProducto,
						PrecioProducto = productoDTO.PrecioProducto,
						Stock = productoDTO.Stock
					};
					context.Productos.Add(mdProducto);
					await context.SaveChangesAsync();
					return Ok();
				}else return BadRequest("ya existe un producto con este codigo");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
		[HttpPut("{id:int}")]

		public async Task<ActionResult> Put(Producto producto, int id)
		{
            if (id != producto.Id) { return BadRequest("id no correspondiente"); }
			var responseApi = new ResponseAPI<int>();
			var mdProducto = await context.Productos.AnyAsync(e => e.Id == id);
            if (!mdProducto)
            {
					responseApi.EsCorrecto = false;
					responseApi.Mensaje = "Producto no encontrado";
                    return NotFound(responseApi.Mensaje);
            }
			responseApi.EsCorrecto = true;
			responseApi.Valor = producto.Id;
			context.Update(producto);
			await context.SaveChangesAsync();
            return Ok(responseApi);		
		}
		
		[HttpDelete]

        public async Task<IActionResult> Delete(int cod)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProducto = await context.Productos.FirstOrDefaultAsync(e => e.CodProducto == cod);
                if (dbProducto != null)
                {
                    context.Productos.Remove(dbProducto);
                    await context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
