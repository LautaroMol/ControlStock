using BlazorCrud.Shared;
using ControlStock.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.BData;
using Stock.BData.Data.Entity;

namespace ControlStock.Server.Controllers
{
    [ApiController]
    [Route("api/Venta")]
    public class VentaController : ControllerBase
    {
        private readonly Context context;

        public VentaController(Context dbcontext)
        {
            context = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Venta>>> Get()
        {
            var venta = await context.Ventas.ToListAsync();
            return venta;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Venta>> GetId(int Id)
        {
            var buscar = await context.Ventas.FirstOrDefaultAsync(c => c.Id == Id);
            if (buscar is null)
            {
                return BadRequest($"venta no encontrada, verifique el id: {Id}");
            }
            return Ok(buscar);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VentaDTO ventaDTO)
        {
            try
            {
                var producto = await context.Productos.FirstOrDefaultAsync(o => o.CodProducto == ventaDTO.CodProducto);

                if (producto is null)
                {
                    // El producto no se encontró, devuelve un error.
                    return BadRequest("El producto no existe.");
                }

                if (producto.Stock < ventaDTO.Cantidad)
                {
                    // Stock insuficiente, devuelve un error.
                    return BadRequest("Stock insuficiente.");
                }

                // Resto la cantidad vendida del stock.
                producto.Stock -= ventaDTO.Cantidad;
                context.Productos.Update(producto);
                await context.SaveChangesAsync();

                var mdVenta = new Venta
                {
                    CodVenta = ventaDTO.CodVenta,
                    FechaVenta = ventaDTO.FechaVenta,
                    Cantidad = ventaDTO.Cantidad,
                    CodProducto = producto.CodProducto,
                };

                context.Ventas.Add(mdVenta);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]

        public async Task<IActionResult> Editar(Venta ventaDTO, int Id)
        {
            var responseApi = new ResponseAPI<int>();
            int diferencia = 0;
            try
            {
                var producto = await context.Productos.FirstOrDefaultAsync(o => o.CodProducto == ventaDTO.CodProducto);
                var mdVenta = await context.Ventas.FirstOrDefaultAsync(e => e.Id == Id);
                if (mdVenta != null & producto != null)
                {
                    diferencia += mdVenta.Cantidad;
                    mdVenta.CodVenta = ventaDTO.CodVenta;
                    mdVenta.CodProducto = producto.Id;
                    mdVenta.FechaVenta = ventaDTO.FechaVenta;
                    mdVenta.Cantidad = ventaDTO.Cantidad;
                    diferencia -= ventaDTO.Cantidad;
                    diferencia = diferencia * -1;
                    producto.Stock -= diferencia;
                    context.Ventas.Update(mdVenta);
                    context.Productos.Update(producto);
                    await context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = mdVenta.CodVenta;
                    responseApi.Mensaje = diferencia.ToString();
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrada";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int Id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var mdVentas = await context.Ventas.FirstOrDefaultAsync(e => e.Id == Id);
                if (mdVentas != null)
                {
                    context.Ventas.Remove(mdVentas);
                    await context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrada";
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
