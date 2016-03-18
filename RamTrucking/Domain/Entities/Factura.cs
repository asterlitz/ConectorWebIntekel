using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum EstatusFactura { Cancelada, Guardada, SinEstatus}
    public class Factura:INotifyPropertyChanged
    {
        private Customer customer;
        public Factura()
        {
            Items = new List<ItemFactura>();
            this.Status = EstatusFactura.SinEstatus;
        }                
        public int FacturaId { get; set; }
        public EstatusFactura Status { get; set; }
        public string IntIdFactura { get; set; }
        public string CustomerName { get; set; }
        public virtual List<ItemFactura> Items { get; set; }
        public virtual Customer Customer
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
                notify("Customer");
            }
        }
        public virtual Invoice Invoice { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void notify(string propertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
