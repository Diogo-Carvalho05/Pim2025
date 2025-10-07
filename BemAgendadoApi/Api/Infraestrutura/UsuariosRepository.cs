
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Infraestrutura
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<UsuariosModels>> GetAllAsync();
        Task<UsuariosModels> GetByIdAsync(int id);
        Task<UsuariosModels> GetByUsernameAsync(string nomeusuario);
        Task AddAsync(UsuariosModels usuario);
        Task UpdateAsync(UsuariosModels usuario);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly AppDbContext _context;

        public UsuariosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuariosModels>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<UsuariosModels> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<UsuariosModels> GetByUsernameAsync(string nomeusuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nomeusuario == nomeusuario);
        }

        public async Task AddAsync(UsuariosModels usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsuariosModels usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == id);
        }
    }
}
