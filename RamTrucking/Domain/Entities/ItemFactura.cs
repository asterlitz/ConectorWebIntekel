using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemFactura:INotifyPropertyChanged
    {
        private string itemName;
        private ItemQB itemQB;
        public int ItemFacturaId { get; set; }
        public string Rfc { get; set; }
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
                notify("ItemName");
            }
        }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public double PrecioUnitario { get; set; }
        public string CantidadEnPieza{ get; set; }
        public double Importe { get; set; }
        public ItemQB ItemQB
        {
            get
            {
                return itemQB;
            }
            set
            {
                itemQB = value;
                notify("ItemQB");
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void notify(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
