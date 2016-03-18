using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFRepositoryItems:IRepositoryItems
    {
        private EFDbContext context = new EFDbContext();
        public EFRepositoryItems()
        {
        }
        public IEnumerable<ItemQB> Items
        {
            get
            {
                return context.QBItems;
            }
        }
        public void register(ItemQB i)
        {
            context.QBItems.Add(i);
            context.SaveChanges();
        }

        public void update(ItemQB i)
        {
            throw new NotImplementedException();
        }

        public void delete(ItemQB i)
        {
            throw new NotImplementedException();
        }
    }
}
