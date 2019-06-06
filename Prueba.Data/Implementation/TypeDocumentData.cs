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
    public class TypeDocumentData:EfBase,  ITypeDocumentData
    {
        public async Task<TypeDocumentDto> GetBy(Expression<Func<TypeDocumentDto, bool>> condition)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<TypeDocument, TypeDocumentDto>());
            return await _context.TypeDocument.ProjectTo<TypeDocumentDto>().FirstOrDefaultAsync(condition);
        }

        public async Task<List<TypeDocumentDto>> GetAll()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<TypeDocument, TypeDocumentDto>());
            return await _context.TypeDocument.ProjectTo<TypeDocumentDto>().ToListAsync();
        }
    }
}
