using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;
using dotnetcoreapi_cake_shop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi_cake_shop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderStatusRepository orderStatusRepository,
            IProductRepository productRepository,
            IShippingMethodRepository shippingMethodRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _shippingMethodRepository = shippingMethodRepository;
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }


        // Get all orders response DTO
        public async Task<List<OrderResponseDto>> GetAllOrders()
        {
            var allOrders = await _orderRepository.GetAllOrders().ToListAsync();

            var allOrderResponseDtos = _mapper.Map<List<OrderResponseDto>>(allOrders);
            return allOrderResponseDtos;
        }

        // Get order response DTO
        public async Task<OrderResponseDto> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            var orderResponseDto = _mapper.Map<OrderResponseDto>(order);
            return orderResponseDto;
        }

        // Create order
        public async Task<OrderResponseDto> CreateOrder(OrderRequestDto orderRequestDto)
        {
            var newOrder = _mapper.Map<Order>(orderRequestDto);

            newOrder.CreateAt = DateTime.UtcNow;

            // Get order item price
            decimal totalItemsPrice = 0;

            if (newOrder.Items != null)
            {
                foreach (var orderItem in newOrder.Items)
                {
                    if (orderItem.ProductId.HasValue)
                    {
                        // Get origin product
                        var product = await _productRepository.GetProductById(orderItem.ProductId.Value);

                        orderItem.Price = product.Price;

                        totalItemsPrice += product.Price * orderItem.Qty;
                    }
                }
            }

            // Get order status
            OrderStatus createdStatus = await _orderStatusRepository.GetOrderStatusByStatus("created");

            if (createdStatus == null)
            {
                // If 'created' status does not exist -> create new 'created' status
                createdStatus = await CreateOrderStatus("Đã tạo đơn hàng", "created");
            }

            newOrder.OrderStatusId = createdStatus.OrderStatusId;

            // Get shipping method and shipping fee
            decimal shippingFee = 0;

            var defaultShippingMethod = (await _shippingMethodRepository
                                                .GetDefaultShippingMethods())
                                                .FirstOrDefault();
            if (defaultShippingMethod != null)
            {
                newOrder.ShippingMethodId = defaultShippingMethod.ShippingMethodId;

                if (orderRequestDto.Distance > defaultShippingMethod.InitialDistance)
                {
                    shippingFee = defaultShippingMethod.InitialCharge 
                                  + (decimal)(Math.Ceiling(orderRequestDto.Distance) - defaultShippingMethod.InitialDistance) * defaultShippingMethod.AdditionalCharge;
                }
                else
                {
                    shippingFee = defaultShippingMethod.InitialCharge;
                }
            }

            newOrder.ShippingFee = shippingFee;

            // Get order total
            newOrder.OrderTotal = totalItemsPrice + shippingFee;

            // Create order
            var createdOrder = await _orderRepository.CreateOrder(newOrder);

            var createdOrderResponseDto = _mapper.Map<OrderResponseDto>(createdOrder);
            return createdOrderResponseDto;
        }

        // Update order status
        public async Task<OrderResponseDto> DeliveryOrder(int id)
        {
            return null;
        }
        public async Task<OrderResponseDto> CancelOrder(int id)
        {
            return null;
        }
        public async Task<OrderResponseDto> SuccessOrder(int id)
        {
            return null;
        }

        // Delete order
        public async Task<OrderResponseDto> DeleteOrder(int orderId)
        {
            var existOrder = await _orderRepository.GetOrderById(orderId);

            if (existOrder == null)
            {
                throw new Exception("order not found");
            }

            var deletedOrder = await _orderRepository.DeleteOrder(existOrder);

            var deletedOrderResponseDto = _mapper.Map<OrderResponseDto>(deletedOrder);
            return deletedOrderResponseDto;
        }


        // Create order status
        private async Task<OrderStatus> CreateOrderStatus(string name, string status)
        {
            var newStatus = new OrderStatus()
            {
                Name = name.ToLower(),
                Status = status.ToLower(),
            };

            var createdOrderStatus = await _orderStatusRepository.CreateOrderStatus(newStatus);

            return createdOrderStatus;
        }
    }
}
