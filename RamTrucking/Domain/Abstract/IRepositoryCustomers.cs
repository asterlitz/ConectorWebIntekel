using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IRepositoryCustomers
    {        
        IEnumerable<Customer> Customers{get;}
        void register(Customer c);
        void update(Customer c);
        void delete(Customer c);        
    }
}
