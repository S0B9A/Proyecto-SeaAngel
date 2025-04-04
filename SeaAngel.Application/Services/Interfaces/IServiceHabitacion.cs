



using SeaAngel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Application.DTOs;

namespace SeaAngel.Application.Services.Interfaces
{
    public interface IServiceHabitacion
    {
        Task<ICollection<HabitacionDTO>> ListAsync();
        Task<HabitacionDTO> FindByIdAsync(int id);
        Task<ICollection<HabitacionDTO>> FindByNameAsync(string nombre);
        Task<ICollection<HabitacionDTO>> FindByNameAndFechaAsync(string nombre, int fechaInicio);
        Task<int> AddAsync(HabitacionDTO dto);
        Task UpdateAsync(int id, HabitacionDTO dto);
        Task<int> GetNextNumber();
    }
}
