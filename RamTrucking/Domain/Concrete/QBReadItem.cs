using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using QBFC13Lib;

namespace Domain.Concrete
{
    public class QBReadItem
    {
        public ItemQB readItemFromCustomerRet(IORItemRet itemRet)
        {
            ItemQB item = new ItemQB();
            if (itemRet.ItemInventoryRet != null)
            {
                item.Name = itemRet.ItemInventoryRet.Name.GetValue().ToString();
                item.QBId = itemRet.ItemInventoryRet.ListID.GetValue().ToString();                
            }
            else if (itemRet.ItemServiceRet != null)
            {
                item.Name = itemRet.ItemServiceRet.Name.GetValue().ToString();
                item.QBId = itemRet.ItemServiceRet.ListID.GetValue().ToString();
            }                        
            return item;
        }        
    }
}
