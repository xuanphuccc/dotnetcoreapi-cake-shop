using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Repositories
{
    public interface IOrderRepository
    {
        // Get all orders
        IQueryable<Order> GetAllOrders();

        // Get order by id
        Task<Order> GetOrderById(int orderId);

        // Create order
        Task<Order> CreateOrder(Order order);

        // Update order
        Task<Order> UpdateOrder(Order order);

        // Update order
        Task<Order> DeleteOrder(Order order);
    }
}
