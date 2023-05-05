using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi_cake_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var allOrderResponseDtos = await _orderService.GetAllOrders();

                return Ok(new ResponseDto()
                {
                    Data = allOrderResponseDtos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "orderId is required" });
            }

            try
            {
                var orderResponseDto = await _orderService.GetOrderById(id.Value);
                if (orderResponseDto == null)
                {
                    return NotFound(new ResponseDto() { Status = 404, Title = "order not found" });
                }

                return Ok(new ResponseDto()
                {
                    Data = orderResponseDto
                });
            }
            catch(Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto orderRequestDto)
        {
            if (orderRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "order is required" });
            }

            try
            {
                // Create order
                var createdOrderResponseDto = await _orderService.CreateOrder(orderRequestDto);

                return CreatedAtAction(
                    nameof(GetOrder),
                    new { id = createdOrderResponseDto.OrderId },
                    new ResponseDto()
                    {
                        Data = createdOrderResponseDto,
                        Status = 201,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }
    }
}
