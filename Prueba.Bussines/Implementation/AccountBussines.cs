using Prueba.Bussines.Interfaces;
using Prueba.Data.Implementation;
using Prueba.Data.Interfaces;
using Prueba.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Prueba.Common;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Prueba.Bussines.Implementation
{
    public class AccountBusiness : IAccountBusiness
    {
        /// <summary>
        /// Instancia de la clase AccountData
        /// </summary>
        IAccountData _iaccountdata;
        IRolData _iroldata;
        ITypeDocumentData _itypedocumentdata;

        /// <summary>
        /// Constructor de la clase
        /// </summary>	
        public AccountBusiness()
        {
            _iaccountdata = new AccountData();
            _iroldata = new RolData();
            _itypedocumentdata = new TypeDocumentData();
        }
        public AccountBusiness(IAccountData iaccountdata, IRolData iroldata, ITypeDocumentData itypedocumentdata)
        {
            this._iaccountdata = iaccountdata;
            this._iroldata = iroldata;
            this._itypedocumentdata = itypedocumentdata;

        }

        public async Task<AccountDto> CreateAsync(AccountDto accountDto)
        {
            try
            {
                accountDto.Password = Extensions.MD5Encrypt(accountDto.Password);
                accountDto.Status = true;
                var result = await _iaccountdata.CreateAsync(accountDto);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<AccountDto> GetBy(Expression<Func<AccountDto, bool>> condition)
        {
            try
            {
                var result = await _iaccountdata.GetBy(condition);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public XmlDocument CreateXML()
        {
            var result =  _iaccountdata.GetAllMongo();
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                      // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(result.Result.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, result.Result);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
            }
            return xmlDoc;
        }
        public async Task<List<AccountDto>> GetAll()
        {
            try
            {
                var result = await _iaccountdata.GetAll();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<AccountDto>> GetAllMongo()
        {
            try
            {
                var result = await _iaccountdata.GetAllMongo();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<RolDto>> GetAllRol()
        {
            try
            {
                var result = await _iroldata.GetAll();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
      
        }
        public async Task<List<TypeDocumentDto>> GetAllType()
        {
            try
            {
                var result = await _itypedocumentdata.GetAll();
                return result;
            }
            catch (Exception )
            {
                return null;
            }
          
        }
        public async Task<VMProfile> GetProfile(long idAccount)
        {
            try
            {
                var account = await _iaccountdata.GetBy(c => c.Id == idAccount);
                //var rol = await _iroldata.GetBy(x => x.Id == account.IdRol);
                var result = new VMProfile()
                {
                    Email = account.Email,
                    Name = account.Name,
                    Status = account.Status,
                    Surname = account.Surname,
                    IdRol = account.IdRol,
                    IdTypeDocument = account.IdTypeDocument,
                    NumberDocument = account.NumberDocument
                };

                return result;
            }
           
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateProfile(VMProfile profile)
        {
            try
            {
                var resultExist = await _iaccountdata.GetById(profile.Id);
                if (resultExist == null) throw new Exception("Not Exist");

                resultExist.Name = profile.Name;
                resultExist.Surname = profile.Surname;
                resultExist.NumberDocument = profile.NumberDocument;
                resultExist.IdRol= profile.IdRol;
                resultExist.IdTypeDocument= profile.IdTypeDocument;

                var result = await _iaccountdata.EditAsync(resultExist);


                return result;
            }
          
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> EditAsync(AccountDto accountdto)
        {
            try
            {
                var resultByEmail = await _iaccountdata.GetById(accountdto.Id);
                if (resultByEmail == null) throw new Exception("Not Exist");
                var edit = await _iaccountdata.EditAsync(accountdto);
                return edit;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(long id)
        {
            try
            {
                await _iaccountdata.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
