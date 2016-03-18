using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace View.Controllers
{
    public class SingletonEmpresaSeleccionada
    {
        private static Empresa empresaActual = null;
        
        public static Empresa getEmpresaSeleccionada()            
        {
            //private const string url2 = @"http://sray.microbit.com/zaroDev/get/get_facturas_zaro.php?idFactura=2"; 
            if(empresaActual == null)
            {
                empresaActual = new Empresa
                {
                    UrlWebServiceFacturas = "http://sray.microbit.com/zaroDev/get/get_facturas_ram.php?idFactura=1"                                        
                };
            }
            return empresaActual;
        }                
    }
}
