namespace BethanysPieShopAdmin.Models.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderDetails(int? id);
        Task<IEnumerable<Order>> GetAllOrderWithDetailsAsync();
    }
}
