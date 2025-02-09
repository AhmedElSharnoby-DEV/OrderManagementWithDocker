using IOrderManagementSystem.Dtos.OrderDtos;
using IOrderManagementSystem.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace IOrderManagementSystem.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            return Ok(await _orderService.CreateOrder(createOrderDto));
        }

    }
}
