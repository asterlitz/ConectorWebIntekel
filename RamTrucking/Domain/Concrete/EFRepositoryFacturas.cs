using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFRepositoryFacturas:IRepositoryFacturas
    {
        private EFDbContext context = new EFDbContext();
        public EFRepositoryFacturas()
        {
        }        

        public IEnumerable<Factura> Facturas
        {
            get            
            {
                return context.Facturas;
            }          
        }

        public IEnumerable<Factura> FacturasCompletas
        {
            get
            {
                return context.Facturas.Include("Items").Include("Invoice").Include("Customer");
            }            
        }

        public void Register(Factura factura)
        {
            context.Facturas.Add(factura);
            context.SaveChanges();
        }

        public void Delete(Factura factura)
        {
            throw new NotImplementedException();
        }

        public void Update(Factura factura)
        {
            throw new NotImplementedException();
        }
        
    }
}
