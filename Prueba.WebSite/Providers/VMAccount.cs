using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.WebSite.Providers
{
    public class VMAccount
    {

        public VMAccount() { }
        public VMAccount(string Email, string Name)
        {
            this.Email = Email;
            this.Name = Name;
        }

        public string Email { get; set; }
        public string Name { get; set; }
    }
}