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

    }
}
