using AutoMapper;
using AutoMapper.QueryableExtensions;
using Prueba.Data.Interfaces;
using Prueba.Entities;
using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Prueba.Data.Implementation
{
    public class RolData : EfBase, IRolData
    {
        public async Task<RolDto> GetBy(Expression<Func<RolDto, bool>> condition)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Rol, RolDto>());
            return await _context.Rol.ProjectTo<RolDto>().FirstOrDefaultAsync(condition);
        }

        public async Task<List<RolDto>> GetAll()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Rol, RolDto>());
            return await _context.Rol.ProjectTo<RolDto>().ToListAsync();
        }
    }
}
