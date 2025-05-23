using API.Models.Data;
using API.Models.Orders;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        public OrderRepository(EcommerceDBContext context) : base(context)
        {
            
        }
    }
}
