using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IRepositoryItems
    {
        IEnumerable<ItemQB> Items { get; }
        void register(ItemQB i);
        void update(ItemQB i);
        void delete(ItemQB i);    
    }
}
