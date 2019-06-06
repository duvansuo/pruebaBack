using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Prueba.Entities.Dto;
using Microsoft.Extensions.Configuration;

namespace Mongo.Entities
{
    public class AccountService: IAccountService
    {
        private readonly MongoClient client = new MongoClient();
      
        public async Task<List<AccountDto>> GetAll()
        {
            var db = client.GetDatabase("Prueba");
            return await db.GetCollection<AccountDto>("Account").AsQueryable().ToListAsync();
        }
        public async Task<AccountDto> GetBy(long id)
        {
            var db = client.GetDatabase("Prueba");
            return await db.GetCollection<AccountDto>("Account").Find(x => x.Id == id).SingleOrDefaultAsync();
        }
        public async Task<bool> Create(AccountDto accountDtos)
        {
            try
            {
                var db = client.GetDatabase("Prueba");
               await  db.GetCollection<AccountDto>("Account").InsertOneAsync(accountDtos);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
 
        }
        public bool Update(AccountDto accountDtos)
        {
            var db = client.GetDatabase("Prueba");
            return  (db.GetCollection<AccountDto>("Account").ReplaceOne(x => x.Id == accountDtos.Id, accountDtos).ModifiedCount>0);
        }        
        public bool Remove(long id)
        {
            var db = client.GetDatabase("Prueba");
            return db.GetCollection<AccountDto>("Account").DeleteOne(x => x.Id == id).DeletedCount>0;
        }
    }
}
