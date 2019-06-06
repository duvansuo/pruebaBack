using Prueba.Bussines.Interfaces;
using Prueba.Entities.Dto;
using Prueba.WebSite.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Prueba.WebSite.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountController : ApiController
    {
        private IAccountBusiness accountbusiness;

        public AccountController(IAccountBusiness accountbusiness)
        {
            this.accountbusiness = accountbusiness;
        }

        public AccountController()
        {
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(AccountDto))]
        public async Task<IHttpActionResult> CreateAsync(AccountDto accountDto)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await accountbusiness.CreateAsync(accountDto);
            if (result != null) return Content(HttpStatusCode.OK, result);
            return Content(HttpStatusCode.InternalServerError, "Error");
        }

        [HttpGet]
        [Route("Rol")]
        public async Task<IHttpActionResult> GetAllRol()
        {
            var result = await accountbusiness.GetAllRol();
            if (result != null)
            {
                return Ok(result);
            }
            return Content(HttpStatusCode.InternalServerError, "Error");
        }

        //[Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await accountbusiness.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return Content(HttpStatusCode.InternalServerError, "Error");
        }



        //[Authorize]
        [HttpGet]
        [Route("GetAllXML")]
        public  IHttpActionResult GetAllXml()
        {
            var result =  accountbusiness.CreateXML();
            if (result != null)
            {
                return Ok(result);
            }
            return Content(HttpStatusCode.InternalServerError, "Error");
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllMongo")]
        public async Task<IHttpActionResult> GetAllMongo()
        {
            var result = await accountbusiness.GetAllMongo();
            if (result != null)
            {
                return Ok(result);
            }
            return Content(HttpStatusCode.InternalServerError, "Error");
        }
        [HttpGet]
        [Route("Type")]
        public async Task<IHttpActionResult> GetAllType()
        {
            var result = await accountbusiness.GetAllType();
            if (result != null)
            {
                return Ok(result);
            }
            return Content(HttpStatusCode.InternalServerError, "Error");
        }
        [Authorize]
        [Route("profile")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProfile()
        {
            // Decode the Claims for get all values
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var idAccount = Convert.ToInt64(claims.Where(c => c.Type == "idAccount").Single().Value);

            var result = await accountbusiness.GetProfile(idAccount);
            if (result != null)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Error");
            }
        }

        [Authorize]
        [Route("profile")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProfile(VMProfile profile)
        {
            // Decode the Claims for get all values
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var idAccount = Convert.ToInt64(claims.Where(c => c.Type == "idAccount").Single().Value);
            profile.Id = idAccount;
            var result = await accountbusiness.UpdateProfile(profile);
            if (result)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Error");
            }
        }

        [Authorize]
        [Route("")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditAsync(AccountDto accountDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await accountbusiness.EditAsync(accountDto);
            if (resultado)
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }

        [Route("checkEmail")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCheckEmail(string email)
        {

            var result = await accountbusiness.GetBy(c => c.Email == email);
            if (result != null)
            {

                return Ok(new VMAccount(result.Email, result.Name));
            }
            return Ok(result);


        }

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(long id)
        {          
            // Decode the Claims for get all values           
            var result = await accountbusiness.Delete(id);
            if (result) return Content(HttpStatusCode.OK, result);           
            return Content(HttpStatusCode.InternalServerError, "Error");
        }
    }
}
