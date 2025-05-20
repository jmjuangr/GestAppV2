using Microsoft.AspNetCore.Mvc;
using GestApp.Business.Services;
using GestApp.Models;

namespace GestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _service;

        public ProductosController(ProductoService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<List<Producto>> Get(
            [FromQuery] string? nombre,
            [FromQuery] decimal? precioMin,
            [FromQuery] decimal? precioMax)
        {
            var productos = _service.FiltrarProductos(nombre, precioMin, precioMax);
            return Ok(productos);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, [FromBody] ProductoCreateDTO dto)
        {
            var actualizado = _service.ActualizarProducto(id, dto);
            if (!actualizado)
            {
                return NotFound($"No se encontró un producto con ID {id}.");
            }

            return Ok($"Producto con ID {id} actualizado correctamente.");
        }


        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var eliminado = _service.EliminarProducto(id);

            if (!eliminado)
            {
                return NotFound($"No se encontró un producto con ID {id}");
            }

            return Ok($"Producto con ID {id} eliminado");
        }


        [HttpPost]
        public IActionResult CrearProducto([FromBody] ProductoCreateDTO dto)
        {
            _service.CrearProducto(dto);
            return Ok("Producto creado correctamente.");
        }




    }
}
