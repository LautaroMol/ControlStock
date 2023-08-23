﻿using BlazorCrud.Shared;
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

        [HttpGet("int:codven")]
        public async Task<ActionResult<Venta>> Getcodven(int codven)
        {
            var buscar = await context.Ventas.FirstOrDefaultAsync(c => c.CodVenta == codven);

            if (buscar is null)
            {
                return BadRequest($"No se encontro la Venta de numero: {codven}");
            }
            return buscar;
        }

        [HttpPost]
        public async Task<IActionResult> Post(VentaDTO ventaDTO)
        {
            try
            {
                var mdVenta = new Venta
                {
                    CodVenta = ventaDTO.CodVenta,
                    ProductoId = ventaDTO.CodProducto,
                    FechaVenta = ventaDTO.FechaVenta,
                    Cantidad = ventaDTO.Cantidad,
                };
                var producto = await context.Productos.FirstOrDefaultAsync(o => o.CodProducto == ventaDTO.CodProducto);
               if (producto is not null) {
                    mdVenta.ProductoNombre = producto.NombreProducto;
                    mdVenta.Producto = producto;
                    mdVenta.Precio = producto.PrecioProducto * mdVenta.Cantidad;
                }
                context.Ventas.Add(mdVenta);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPut]

        public async Task<IActionResult> Editar(VentaDTO ventaDTO, int codVenta)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var producto = await context.Productos.FirstOrDefaultAsync(o => o.CodProducto == ventaDTO.CodProducto);
                var mdVenta = await context.Ventas.FirstOrDefaultAsync(e => e.CodVenta == codVenta);
                if (mdVenta != null)
                {
                    mdVenta.CodVenta = (int)ventaDTO.CodVenta;
                    mdVenta.ProductoNombre = ventaDTO.Producto;
                    mdVenta.ProductoId = producto.Id;
                    mdVenta.Precio = (int)ventaDTO.Precio;
                    mdVenta.FechaVenta = ventaDTO.FechaVenta;
                    mdVenta.Cantidad = ventaDTO.Cantidad;
                    context.Ventas.Update(mdVenta);
                    await context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = mdVenta.CodVenta;
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

        [HttpDelete]

        public async Task<IActionResult> Delete(int codVen)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var mdVentas = await context.Ventas.FirstOrDefaultAsync(e => e.CodVenta == codVen);
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