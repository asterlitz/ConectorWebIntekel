using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public EFDbContext()
            : base("name=EFDbContext")
        {
        }
        public DbSet<Customer> QBCustomers { get; set; }
        public DbSet<ItemQB> QBItems { get; set; }
        public DbSet<Factura> Facturas{ get; set; }
    }
}
