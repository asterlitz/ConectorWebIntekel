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
    public class ControllerFactura
    {
        private IRepositoryCustomers repositoryCustomers;
        private IRepositoryItems repositoryItems;
        private IRepositoryFacturas repositoryFacturas;
        public ControllerFactura()
        {
            repositoryCustomers = new EFRepositoryCustomers();
            repositoryItems = new EFRepositoryItems();
            repositoryFacturas = new EFRepositoryFacturas();
        }
        public IEnumerable<Factura> cargarFacturas(string urlWs)
        {
            JsonRepositoryFacturas repository = new JsonRepositoryFacturas(urlWs);
            return repository.Facturas;
        }
        public Factura facturaToInvoice(Factura factura)
        {
            if (!existeFactura(factura))
            {
                searchCustomer(factura);
                searchItems(factura);
            }
            else
            {
                factura = repositoryFacturas.FacturasCompletas.Where(f => f.IntIdFactura.Equals(factura.IntIdFactura)).FirstOrDefault();   
            }
            return factura;
        }
        public void saveFactura(Factura factura)
        {            
            QBInvoices qbInvoices = new QBInvoices();            
            Invoice invoice = qbInvoices.saveInvoice(factura);
            factura.CustomerName = factura.Customer.Name;
            factura.Invoice = invoice;
            factura.Status = EstatusFactura.Guardada;
            this.repositoryFacturas.Register(factura);
        }
        private bool existeFactura(Factura factura)
        {
            Factura facturaBuscada = repositoryFacturas.Facturas.Where(f => f.IntIdFactura == factura.IntIdFactura).FirstOrDefault();
            bool existe = (facturaBuscada != null);                     
            return existe;
        }

        private void searchCustomer(Factura factura)
        {
            factura.Customer = repositoryCustomers.Customers.Where(c => c.Name == factura.CustomerName).FirstOrDefault();
        }

        private void searchItems(Factura factura)
        {
            foreach (var item in factura.Items)
            {
                item.ItemQB = repositoryItems.Items.Where(i => i.Name == item.ItemName).FirstOrDefault();
            }
        }

        public void cancelFactura(Invoice invoice, Customer customer)
        {            
            QBInvoices qbInvoice = new QBInvoices();
            qbInvoice.cancelInvoice(invoice, customer);
        }

        public void deleteFactura(Invoice invoice)
        {
            QBInvoices qbInvoice = new QBInvoices();
            qbInvoice.deleteInvoice(invoice);
        }
    }
}
