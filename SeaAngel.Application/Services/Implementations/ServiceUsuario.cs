﻿using AutoMapper;
using Libreria.Application.Config;
using Libreria.Application.Utils;
using Microsoft.Extensions.Options;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper, IOptions<AppConfig> options)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
        }

        public async Task<string> AddAsync(UsuarioDTO dto)
        {
            // Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(dto.Contraseña!, secret);
            // Establecer password DTO
            dto.Contraseña = passwordEncrypted;
            var objectMapped = _mapper.Map<Usuario>(dto);

            return await _repository.AddAsync(objectMapped);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<ICollection<UsuarioDTO>> FindByDescriptionList(string description)
        {
            var list = await _repository.FindByDescriptionList(description);
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            return collection;
        }
        public async Task<UsuarioDTO> FindByDescription(string description)
        {
            var @object = await _repository.FindByDescription(description);
            var objectMapped = _mapper.Map<UsuarioDTO>(@object);
            return objectMapped;
        }
        public async Task<UsuarioDTO> FindByIdAsync(string id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<UsuarioDTO>(@object);
            return objectMapped;
        }


        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            // Return Data
            return collection;
        }

        public async Task<UsuarioDTO> LoginAsync(string id, string password)
        {
            UsuarioDTO usuarioDTO = null!;

            // Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(password, secret);

            var @object = await _repository.LoginAsync(id, passwordEncrypted);

            if (@object != null)
            {
                usuarioDTO = _mapper.Map<UsuarioDTO>(@object);
            }

            return usuarioDTO;
        }

        public async Task UpdateAsync(string id, UsuarioDTO dto)
        {
            var @object = await _repository.FindByIdAsync(id);
            //       source, destination
            _mapper.Map(dto, @object!);
            await _repository.UpdateAsync();
        }
    }
}
