using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Empresa
    {                
        public string UrlWebServiceFacturas { get; set; }
        public string UrlWebServiceFacturasCanceladas { get; set; }
        public string Nombre { get; set; }
    }
}
