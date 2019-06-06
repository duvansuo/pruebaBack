using Microsoft.Owin.Security.OAuth;
using Prueba.Bussines.Implementation;
using Prueba.Bussines.Interfaces;
using System.Threading.Tasks;
using Prueba.Common;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace Prueba.WebSite
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        IAccountBusiness AccountBusiness { get { return new AccountBusiness(); } }
      

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var UserName = context.UserName;
            var password = Extensions.MD5Encrypt(context.Password);

            var resultPerson = await AccountBusiness.GetBy(c => c.Email == UserName && c.Password == password);
            if (resultPerson == null)
            {
                context.SetError("invalid_grant", "El usuario o la contraseña es incorrecto");
                return;
            }



            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim("idAccount", resultPerson.Id.ToString()));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {                  
                    {
                        "idAccount", resultPerson.Id.ToString()
                    }, {
                        "rol", resultPerson.IdRol.ToString()
                    },
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);

            }
            return Task.FromResult<object>(null);
        }
    }
}