﻿using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPago
    {
        Task<ICollection<Pago>> ListAsync();
        Task<Pago> FindByIdAsync(int id);
        Task<int> AddAsync(Pago entity);
    }
}
