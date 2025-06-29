using GestApp.Models;
using GestApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GestApp.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly GestAppDbContext _context;

        public UsuarioRepository(GestAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {

            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            // Busca un usuario 
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            // Añade un nuevo usuario 
            _context.Usuarios.Add(usuario);


            await _context.SaveChangesAsync();


            return usuario;
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            //update de usuario
            _context.Usuarios.Update(usuario);


            await _context.SaveChangesAsync();


            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Busca el usuario por ID
            var usuario = await _context.Usuarios.FindAsync(id);


            if (usuario == null) return false;


            _context.Usuarios.Remove(usuario);


            await _context.SaveChangesAsync();


            return true;
        }
    }
}
