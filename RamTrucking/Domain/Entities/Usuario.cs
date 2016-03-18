using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public TipoUsuario TipoUsuario{get; set; }
    }
}
