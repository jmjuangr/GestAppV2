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

        public ProductosController()
        {
            _service = new ProductoService();
        }

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            var productos = _service.ObtenerTodos();
            return Ok(productos);
        }
    }
}
