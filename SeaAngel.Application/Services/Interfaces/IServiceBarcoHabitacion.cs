using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Application.DTOs;

namespace SeaAngel.Application.Services.Interfaces
{
    public interface IServiceBarcoHabitacion
    {
        Task<BarcoHabitacionDTO> FindByIdAsync(int idbarco, int idhabitacion);
    }
}
