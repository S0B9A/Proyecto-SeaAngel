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
    public class RepositoryFecha : IRepositoryFecha
    {
        private readonly SeanAngelContext _context;

        public RepositoryFecha(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Fecha> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Fecha>()
                .Include(a => a.Idcrucero)
                .Include(a => a.FechaInicio)
                .Include(a => a.FechaLimitePago)
                .Include(a => a.IdcruceroNavigation)
                .Include(a => a.FechaHabitacion)
                    .ThenInclude(b => b.IdhabitacionNavigation)

                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return @object!;
        }

        public async Task<ICollection<Fecha>> ListAsync()
        {
            // Obtener los Puertoa e incluir la relación Itinerario
            var collection = await _context.Set<Fecha>()
                .Include(a => a.Idcrucero)
                .Include(a => a.FechaInicio)
                .Include(a => a.FechaLimitePago)
                .Include(a => a.IdcruceroNavigation)
                .Include(a => a.FechaHabitacion)
                    .ThenInclude(b => b.IdhabitacionNavigation)
                .ToListAsync();

            return collection;
        }

        public async Task<int> AddAsync(Fecha entity)
        {

            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Agregar el barco a la base de datos
                await _context.Set<Fecha>().AddAsync(entity);

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
        public async Task<int> GetNextNumber()
        {
            int current = 0;

            string sql = string.Format("SELECT IDENT_CURRENT ('Fecha') AS Current_Identity;");

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
