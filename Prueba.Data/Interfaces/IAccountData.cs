using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Data.Interfaces
{
    public interface IAccountData 
    {
        Task<AccountDto> CreateAsync(AccountDto accountDto);
        Task<AccountDto> GetBy(Expression<Func<AccountDto, bool>> condition);
        Task<AccountDto> GetById(long id);
        Task<List<AccountDto>> GetAll();
        Task<bool> EditAsync(AccountDto accountDto);
        Task<bool> Delete(long id);
        Task<List<AccountDto>> GetAllMongo();
    }
}
