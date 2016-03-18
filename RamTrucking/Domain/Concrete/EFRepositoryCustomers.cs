using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFRepositoryCustomers:IRepositoryCustomers
    {
        private EFDbContext context = new EFDbContext();
        public EFRepositoryCustomers()
        {
        }
        public IEnumerable<Customer> Customers
        {
            get
            {
                return context.QBCustomers;
            }
        }
        public void register(Customer c)
        {
            context.QBCustomers.Add(c);
            context.SaveChanges();
        }
        public void update(Customer c)
        {
            throw new NotImplementedException();
        }
        public void delete(Customer c)
        {
            throw new NotImplementedException();
        }        
    }
}
