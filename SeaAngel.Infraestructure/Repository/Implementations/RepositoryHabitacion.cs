using Microsoft.EntityFrameworkCore;
using SeaAngel.Infraestructure.Data;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeaAngel.Infraestructure.Data;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;

namespace SeaAngel.Infraestructure.Repository.Implementations
{
    public class RepositoryHabitacion : IRepositoryHabitacion
    {

        private readonly SeanAngelContext _context;

        public RepositoryHabitacion(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Habitacion> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Habitacion>()
                                      .Where(x => x.Id == id)
                                      .Include(b => b.BarcoHabitacion)
                                      .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<Habitacion>> ListAsync()
        {
            var collection = await _context.Set<Habitacion>().AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task<ICollection<Habitacion>> FindByNameAsync(string nombre)
        {
            var collection = await _context
                                         .Set<Habitacion>()
                                         .Where(p => p.Nombre.Contains(nombre))
                                         .ToListAsync();
            return collection;
        }
        public async Task<int> AddAsync(Habitacion entity)
        {

            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Agregar el barco a la base de datos
                await _context.Set<Habitacion>().AddAsync(entity);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await _context.Database.CommitTransactionAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }

        }
        public async Task UpdateAsync(Habitacion entity)
        {
            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Actualizar el barco en la base de datos
                _context.Set<Habitacion>().Update(entity);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // Si hay un error, hacer rollback de la transacción
                await _context.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetNextNumber()
        {
            int current = 0;

            string sql = string.Format("SELECT IDENT_CURRENT ('Habitacion') AS Current_Identity;");

            System.Data.DataTable dataTable = new System.Data.DataTable();

            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;
            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;
                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }


            current = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            return await Task.FromResult(current);
        }
    }
}
