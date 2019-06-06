using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Data.Interfaces
{
    public interface ITypeDocumentData
    {
        Task<TypeDocumentDto> GetBy(Expression<Func<TypeDocumentDto, bool>> condition);
        Task<List<TypeDocumentDto>> GetAll();
    }
}
