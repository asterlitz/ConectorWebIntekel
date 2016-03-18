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
    public class ControllerItem
    {
        private QBItem qbItem;
        private IRepositoryItems repositoryItems;
        public ControllerItem()
        {
            qbItem = new QBItem();
            repositoryItems = new EFRepositoryItems();
        }
        public IEnumerable<ItemQB> loadItemsFromQB()
        {
            return qbItem.getItems();
        }
        public IEnumerable<ItemQB> loadItemsFromDatabase()
        {
            return repositoryItems.Items;
        }
        public void registerItemInDatabase(ItemQB i)
        {
            repositoryItems.register(i);
        }
    }
}