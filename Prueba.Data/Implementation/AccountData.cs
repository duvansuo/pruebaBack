using AutoMapper;
using AutoMapper.QueryableExtensions;
using Prueba.Data.Interfaces;
using Prueba.Entities;
using Mongo.Entities;
using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Data.Implementation
{
    public class AccountData : EfBase, IAccountData
    {
        private readonly IAccountService _accountService;
        public AccountData()
        {
            _accountService = new AccountService();
        }
        public async Task<AccountDto> CreateAsync(AccountDto accountDto)
        {



            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<AccountDto, Account>());
            var entity = Mapper.Map<AccountDto, Account>(accountDto);
            _context.Account.Add(entity);
            await _context.SaveChangesAsync();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Account, AccountDto>());
            var result =  Mapper.Map<Account, AccountDto>(entity);
            await _accountService.Create(result);
            return result;
        }

        public async Task<AccountDto> GetBy(Expression<Func<AccountDto, bool>> condition)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Account, AccountDto>());
            var result =  await _context.Account.ProjectTo<AccountDto>().FirstOrDefaultAsync(condition);
            var resultMongo = await _accountService.GetBy(result.Id);            
            return result;
        }

        public async Task<AccountDto> GetById(long id)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Account, AccountDto>());
            var entity = await _context.Account.FindAsync(id);
            var result = Mapper.Map<Account, AccountDto>(entity);
            return result;
        }
        public async Task<List<AccountDto>> GetAll()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Account, AccountDto>());
            var result = await _context.Account.ProjectTo<AccountDto>().ToListAsync();
            return result;
        }
        public async Task<List<AccountDto>> GetAllMongo()
        {
            var result = await _accountService.GetAll();           
            return result;
        }
        public async Task<bool> EditAsync(AccountDto accountDto)
        {          
            var entity = await _context.Account.Where(c => c.Id == accountDto.Id).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Email = accountDto.Email;
                entity.Status = accountDto.Status;
                entity.Name = accountDto.Name;
                entity.Surname = accountDto.Surname;
                entity.IdRol = accountDto.IdRol;
                entity.NumberDocument = accountDto.NumberDocument;
                entity.IdTypeDocument = accountDto.IdTypeDocument;
                await _context.SaveChangesAsync();
             
               _accountService.Update(accountDto);
                return true;

            }

            return false;

        }
        public async Task<bool> Delete(long id)
        {
            var entity = _context.Account.Find(id);
            if (entity != null)
            {
                _context.Account.Remove(entity);
                await _context.SaveChangesAsync();
                _accountService.Remove(id);

                return true;
            }
            return false;
        }
    }
}
