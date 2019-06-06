using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Entities
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAll();
        Task<AccountDto> GetBy(long id);
        Task<bool> Create(AccountDto accountDtos);
        bool Update(AccountDto accountDtos);
        bool Remove(long id);
    }
}
