using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Prueba.Bussines.Interfaces
{
    public interface IAccountBusiness
    {
        Task<AccountDto> CreateAsync(AccountDto accountDto);
        Task<AccountDto> GetBy(Expression<Func<AccountDto, bool>> condition);
        Task<List<AccountDto>> GetAll();
        Task<List<AccountDto>> GetAllMongo();
        Task<List<RolDto>> GetAllRol();
        Task<List<TypeDocumentDto>> GetAllType();
        Task<VMProfile> GetProfile(long idAccount);
        Task<bool> UpdateProfile(VMProfile profile);

        Task<bool> EditAsync(AccountDto accountdto);
        Task<bool> Delete(long id);
        XmlDocument CreateXML();
    }
}
