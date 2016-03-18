using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using QBFC13Lib;

namespace Domain.Concrete
{
    public class QBItem
    {
        private IItemQuery itemQuery;           
        private QBSession apiQBSession;
        private QBReadItem readItem;
        public QBItem()
        {
            apiQBSession = QBFactorySession.getInstance();
        }                
        public IEnumerable<ItemQB> getItems()
        {            
            IEnumerable<ItemQB> items = null;
            return query(ref items);
        }      
        private IEnumerable<ItemQB> getItem(string name)
        {            
            IEnumerable<ItemQB> items = null;
            this.setFilter(name);
            return query(ref items);
        }
        private IEnumerable<ItemQB> query(ref IEnumerable<ItemQB> items)
        {
            initializeObjects();
            if (apiQBSession.executeQuery())
            {
                items = readItems((IORItemRetList)apiQBSession.getResponse());
            }
            return items;
        }
        public void setFilter(string name)
        {
            itemQuery.ORListQuery.ListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains);
            itemQuery.ORListQuery.ListFilter.ORNameFilter.NameFilter.Name.SetValue(name);            
        }
        private void initializeObjects()
        {
            readItem = new QBReadItem();
            itemQuery = apiQBSession.getRequestMsgSet().AppendItemQueryRq();
            itemQuery.IncludeRetElementList.Add("ListID");
            itemQuery.IncludeRetElementList.Add("Name");
            itemQuery.IncludeRetElementList.Add("FullName");
        }
        private List<ItemQB> readItems(IORItemRetList itemRetList)
        {            
            List<ItemQB> items = new List<ItemQB>();
            for (int i = 0; i < itemRetList.Count; i++)
            {
                IORItemRet cutomerRet = itemRetList.GetAt(i);
                ItemQB item = readItem.readItemFromCustomerRet(cutomerRet);
                items.Add(item);
            }
            return items;
        }        
    }
}   