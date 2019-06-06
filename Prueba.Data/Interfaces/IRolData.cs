using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Data.Interfaces
{
    public interface IRolData
    {
        Task<RolDto> GetBy(Expression<Func<RolDto, bool>> condition);
        Task<List<RolDto>> GetAll();
    }
}
