using Microsoft.EntityFrameworkCore;
using SeaAngel.Infraestructure.Data;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Infraestructure.Repository.Implementations
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly SeanAngelContext _context;

        public RepositoryUsuario(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<string> AddAsync(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.CorreoElectronico;
        }

        public async Task DeleteAsync(string id)
        {

            var @object = await FindByIdAsync(id);
            _context.Remove(@object);
            _context.SaveChanges();
        }

        public async Task<ICollection<Usuario>> FindByDescriptionList(string description)
        {
            var collection = await _context
                                         .Set<Usuario>()
                                         .Where(p => p.CorreoElectronico.Contains(description))
                                         .ToListAsync();
            return collection;
        }
        public async Task<Usuario> FindByDescription(string description)
        {
            var @object = await _context
                         .Set<Usuario>()
                         .FirstOrDefaultAsync(p => p.CorreoElectronico.Contains(description));

            return @object;
        }
        public async Task<Usuario> FindByIdAsync(string id)
        {
            var @object = await _context.Set<Usuario>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            var collection = await _context.Set<Usuario>()
                                          .ToListAsync();
            return collection;
        }

        public async Task<Usuario> LoginAsync(string id, string password)
        {
            var @object = await _context.Set<Usuario>()
                                        .Where(p => p.CorreoElectronico == id && p.Contraseña == password)
                                        .FirstOrDefaultAsync();
            return @object!;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
