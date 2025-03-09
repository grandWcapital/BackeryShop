
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAdmin.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BethanysPieShopDbContext _context;
        public OrderRepository(BethanysPieShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrderWithDetailsAsync()
        {
           return await _context.Orders.Include(x=>x.OrderDetails).ThenInclude(od=>od.Pie).OrderBy(x=>x.OrderId).ToListAsync();
        }

        public async Task<Order?> GetOrderDetails(int? id)
        {
            if(id != null)
            {
                var order = await _context.Orders.Include(x => x.OrderDetails).ThenInclude(od => od.Pie).OrderBy(x=>x.OrderId).Where(o => o.OrderId== id.Value).FirstOrDefaultAsync();

                return order;
            }
            return null;
        }
    }
}
