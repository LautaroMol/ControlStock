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

        [HttpGet("int:CodProducto")]
        public async Task<ActionResult<Producto>> GetProdCodigo(int codigo)
        {
            var buscar = await context.Productos.FirstOrDefaultAsync(c => c.CodProducto == codigo);
            if (buscar is null)
            {
                return BadRequest($"Producto no encontrado, verifique el codigo: {codigo}");
            }
            return Ok(buscar);
        }

        [HttpPost]

        public async Task<IActionResult> Post(ProductoDTO productoDTO)
        {
            try
            {
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
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPut]

        public async Task<IActionResult> Editar(ProductoDTO productoDTO, int cod)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var mdProducto = await context.Productos.FirstOrDefaultAsync(e => e.CodProducto == cod);
                if (mdProducto != null)
                {
                    mdProducto.NombreProducto = productoDTO.NombreProducto;
                    mdProducto.PrecioProducto = productoDTO.PrecioProducto;
                    mdProducto.Stock = productoDTO.Stock;
                    context.Productos.Update(mdProducto);
                    await context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = mdProducto.CodProducto;
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
