using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IRepositoryFacturas
    {
        IEnumerable<Factura> Facturas { get; }
        IEnumerable<Factura> FacturasCompletas { get;}
        void Register(Factura factura);
        void Delete(Factura factura);
        void Update(Factura factura);
    }
}