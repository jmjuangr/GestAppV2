using Microsoft.AspNetCore.Mvc;
using GestApp.Business.Services;
using GestApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace GestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            // Recuper usuarios 
            var usuarios = await _service.GetAllAsync();


            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            // Busca usuareio por id
            var usuario = await _service.GetByIdAsync(id);


            if (usuario == null) return NotFound();


            return Ok(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDTO>> Create(UsuarioCreateDTO dto)
        {
            // un nuevo usuario con datos del DTO
            var creado = await _service.AddAsync(dto);


            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UsuarioCreateDTO dto)
        {

            var actualizado = await _service.UpdateAsync(id, dto);


            if (!actualizado) return NotFound();


            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {

            var ok = await _service.DeleteAsync(id);


            if (!ok) return NotFound();

            return NoContent();
        }
    }
}
