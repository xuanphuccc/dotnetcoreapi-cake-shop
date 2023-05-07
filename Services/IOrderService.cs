using dotnetcoreapi_cake_shop.Dtos;

namespace dotnetcoreapi_cake_shop.Services
{
    public interface IOrderService
    {
        // Get all orders response DTO
        Task<List<OrderResponseDto>> GetAllOrders();

        // Get order response DTO
        Task<OrderResponseDto> GetOrderById(int orderId);

        // Create order
        Task<OrderResponseDto> CreateOrder(OrderRequestDto orderRequestDto);

        // Update order status
        Task<OrderResponseDto> DeliveryOrder(int orderId);
        Task<OrderResponseDto> CancelOrder(int orderId);
        Task<OrderResponseDto> SuccessOrder(int orderId);

        // Delete order
        Task<OrderResponseDto> DeleteOrder(int orderId);
    }
}
