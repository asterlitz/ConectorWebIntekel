using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invoice
    {        
        public int InvoiceId { get; set; }
        public DateTime TxnDate { get; set; }
        public string QBId { get; set; }
        public string EditSequence { get; set; }
    }
}
