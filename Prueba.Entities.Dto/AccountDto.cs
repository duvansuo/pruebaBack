using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entities.Dto
{
    public class AccountDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public int IdTypeDocument { get; set; }
        public string NumberDocument { get; set; }

    }
}
