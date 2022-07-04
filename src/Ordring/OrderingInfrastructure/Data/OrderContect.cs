using Microsoft.EntityFrameworkCore;
using OrderingCore.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingInfrastructure.Data
{
  public  class OrderContect : DbContext
    {

        public OrderContect(DbContextOptions<OrderContect> options):base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
