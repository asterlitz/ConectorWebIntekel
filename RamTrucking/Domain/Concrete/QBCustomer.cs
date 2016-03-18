using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using QBFC13Lib;

namespace Domain.Concrete
{
    public class QBCustomer
    {        
        private ICustomerQuery customerQuery;           
        private QBSession apiQBSession;
        private QBReadCustomer readBillPayment;
        public QBCustomer()
        {
            apiQBSession = QBFactorySession.getInstance();
        }        
        private void initializeObjects()
        {
            readBillPayment = new QBReadCustomer();
            customerQuery = apiQBSession.getRequestMsgSet().AppendCustomerQueryRq();
            customerQuery.IncludeRetElementList.Add("ListID");
            customerQuery.IncludeRetElementList.Add("Name");            
        }
        public IEnumerable<Customer> getCustomers()
        {
            IEnumerable<Customer> customers = null;
            return query(ref customers);
        }
        public IEnumerable<Customer> getCustomer(string name)
        {
            IEnumerable<Customer> customers = null;            
            this.setFilter(name);
            return query(ref customers);
        }                     
        private IEnumerable<Customer> query(ref IEnumerable<Customer> customers)
        {
            initializeObjects();
            if (apiQBSession.executeQuery())
            {
                customers = readCustomers((ICustomerRetList)apiQBSession.getResponse());
            }
            return customers;
        }
        public void setFilter(string name)
        {
            customerQuery.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains);
            customerQuery.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.Name.SetValue(name);
        }
        private List<Customer> readCustomers(ICustomerRetList customerRetList)
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < customerRetList.Count; i++)
            {
                ICustomerRet cutomerRet = customerRetList.GetAt(i);
                Customer customer = readBillPayment.readCustomerFromCustomerRet(cutomerRet);
                customers.Add(customer);
            }
            return customers;
        }        
    }
}