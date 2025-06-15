using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestApp.Models;
using GestApp.Data;

namespace GestApp.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;


        private readonly GestAppDbContext _context;

        // recibe las dependencias 
        public AuthController(IConfiguration config, GestAppDbContext context)
        {
            _config = config;
            _context = context;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO dto)
        {
            // Busca en bbdd un usuario con el nombre y contraseña recibidos
            var usuario = _context.Set<Usuario>().FirstOrDefault(u =>
                u.Nombre == dto.Nombre && u.Pass == dto.Pass);


            if (usuario == null)
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });

            // Si ok genera un token y lo devuelve
            var token = GenerarToken(usuario);
            return Ok(new { token });
        }

        // Método  que genera un token JWT 
        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            // Genera la clave secreta 
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            // CCrea credenciales
            var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            // Crea tiken
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // El token dura 2 horas
                signingCredentials: credenciales
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UsuarioRegisterDTO dto)
        {
            // Verificamos si ya existe un usuario con ese nombre
            var usuarioExistente = _context.Set<Usuario>()
                .FirstOrDefault(u => u.Nombre == dto.Nombre);


            if (usuarioExistente != null)
                return Conflict(new { mensaje = "Ese nombre de usuario ya está en uso." });

            // Crea un nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Pass = dto.Pass,
                Rol = dto.Rol
            };

            // Añadimos el nuevo usuario a bbdd
            _context.Set<Usuario>().Add(nuevoUsuario);
            _context.SaveChanges();


            return Ok(new { mensaje = "Usuario registrado correctamente" });
        }
    }
}
