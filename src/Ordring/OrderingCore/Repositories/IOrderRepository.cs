using OrderingCore.Entites;
using OrderingCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingCore.Repositories
{
   public interface IOrderRepository :IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);

    }
}
