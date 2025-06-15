using GestApp.Data.Repositories;
using GestApp.Models;
using GestApp.Models.DTOs;

namespace GestApp.Business.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Rol = u.Rol
            });
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var u = await _repository.GetByIdAsync(id);
            if (u == null) return null;

            return new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Rol = u.Rol
            };
        }

        public async Task<UsuarioDTO> AddAsync(UsuarioCreateDTO dto)
        {
            var nuevo = new Usuario
            {
                Nombre = dto.Nombre,
                Pass = dto.Password,
                Rol = dto.Rol
            };

            var creado = await _repository.AddAsync(nuevo);

            return new UsuarioDTO
            {
                Id = creado.Id,
                Nombre = creado.Nombre,
                Rol = creado.Rol
            };
        }

        public async Task<bool> UpdateAsync(int id, UsuarioCreateDTO dto)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return false;

            usuario.Nombre = dto.Nombre;
            usuario.Pass = dto.Password;
            usuario.Rol = dto.Rol;

            return await _repository.UpdateAsync(usuario);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
