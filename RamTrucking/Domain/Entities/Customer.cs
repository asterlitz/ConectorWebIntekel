using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer:INotifyPropertyChanged
    {
        private string name;        
        public int CustomerId { get; set; }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                notify("Name");
            }
        }
        public string QBId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void notify(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}
