using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
namespace View.Controllers
{
    public class ControllerCustomer
    {
        private QBCustomer qbCustomer;
        private IRepositoryCustomers repositoryCustomer;
        public ControllerCustomer()
        {
            qbCustomer = new QBCustomer();
            repositoryCustomer = new EFRepositoryCustomers();
        }
        public IEnumerable<Customer> loadCustomersFromQB()
        {
            return qbCustomer.getCustomers();
        }
        public IEnumerable<Customer> loadCustomersFromDatabase()
        {
            return repositoryCustomer.Customers;
        }        
        public void registerCustomerInDatabase(Customer c)
        {
            repositoryCustomer.register(c);
        }
    }
}
