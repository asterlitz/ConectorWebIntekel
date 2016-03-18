using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using QBFC13Lib;
namespace Domain.Concrete
{
    public class QBInvoices
    {
        private IInvoiceAdd invoiceAdd;
        private IInvoiceRet invoiceRet;        
        private QBSession qBSession;
        public QBInvoices()
        {
            qBSession = QBFactorySession.getInstance();            
        }
        public Invoice saveInvoice(Factura factura)
        {
            Invoice invoice = new Invoice();
            initializeObjects();
            addCustomer(factura.Customer);
            addItems(factura);
            executeQuery(invoice);

            return invoice;
        }
        public bool cancelInvoice(Invoice invoice, Customer customer)
        {
            IInvoiceMod modify = qBSession.getRequestMsgSet().AppendInvoiceModRq();
            modify.CustomerRef.ListID.SetValue(customer.QBId); 
            modify.TxnID.SetValue(invoice.QBId);
            modify.EditSequence.SetValue(invoice.EditSequence);
            modify.IsPending.SetValue(true);            
            return qBSession.executeQuery();
        }
        public bool deleteInvoice(Invoice invoice)
        {
            ITxnDel txn = qBSession.getRequestMsgSet().AppendTxnDelRq();
            txn.TxnDelType.SetValue(ENTxnDelType.tdtInvoice);
            txn.TxnID.SetValue(invoice.QBId);
            return this.qBSession.executeQuery();
        }
        private void initializeObjects()
        {
            invoiceAdd = qBSession.getRequestMsgSet().AppendInvoiceAddRq();                        
        }
        private void addCustomer(Customer customer)
        {            
            invoiceAdd.CustomerRef.ListID.SetValue(customer.QBId);              
        }
        private void addItems(Factura factura)
        {
            for (int i = 0; i < factura.Items.Count; i++)
            {
                addItem(factura.Items[i]);
            }
        }
        private void addItem(ItemFactura item)
        {
            IORInvoiceLineAdd invoiceLineAdd = invoiceAdd.ORInvoiceLineAddList.Append();            
            invoiceLineAdd.InvoiceLineAdd.ItemRef.ListID.SetValue(item.ItemQB.QBId);            
            invoiceLineAdd.InvoiceLineAdd.Quantity.SetValue(int.Parse(item.CantidadEnPieza));            
            invoiceLineAdd.InvoiceLineAdd.ORRatePriceLevel.Rate.SetValue(Math.Round(item.PrecioUnitario));
            invoiceLineAdd.InvoiceLineAdd.Amount.SetValue(Math.Round(item.Importe, 2));
        }
        private void executeQuery(Invoice invoice)
        {
            if (qBSession.executeQuery())
                invoiceRet = (IInvoiceRet)qBSession.getResponse();

            invoice.TxnDate = invoiceRet.TxnDate.GetValue();
            invoice.QBId = invoiceRet.TxnID.GetValue();
            invoice.EditSequence = invoiceRet.EditSequence.GetValue();
        }          
    }
}
