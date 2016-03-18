using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using QBFC13Lib;

namespace Domain.Concrete
{
    public class QBReadCustomer
    {
        public Customer readCustomerFromCustomerRet(ICustomerRet customerRet)
        {
            Customer customer = new Customer();
            customer.Name = customerRet.Name.GetValue().ToString();
            customer.QBId = customerRet.ListID.GetValue().ToString();
            return customer;
        }
    }
}
