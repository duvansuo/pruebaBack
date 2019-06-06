using Microsoft.Practices.Unity;
using Prueba.Bussines.Implementation;
using Prueba.Bussines.Interfaces;
using Prueba.Data.Implementation;
using Prueba.Data.Interfaces;
using System.Web.Http;

using Unity.WebApi;

namespace Prueba.WebSite
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            #region DI Data Layer

            container.RegisterType<IAccountData, AccountData>();
            container.RegisterType<IRolData, RolData>();
            container.RegisterType<ITypeDocumentData, TypeDocumentData>();


            #endregion

            #region DI Business Layer
            container.RegisterType<IAccountBusiness, AccountBusiness>();
          
            #endregion

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}