using CoptisTest.API.Repositories;
using CoptisTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoptisTest.API.Controllers
{
    [ApiController]
    [Route("Order/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                return Ok(await _orderRepository.GetOrders());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId)
        {
            try
            {
                var result = await _orderRepository.GetOrder(orderId);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            try
            {
                if (order == null) return BadRequest();

                await _orderRepository.AddOrder(order);

                return CreatedAtAction(nameof(GetOrder), new { OrderId = order.OrderId }, null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult<Order>> DeleteOrder(int orderId)
        {
            try
            {
                var orderToDelete = await _orderRepository.GetOrder(orderId);

                if (orderToDelete == null)
                {
                    return NotFound($"Order with Id = {orderId} not found");
                }
                await _orderRepository.DeleteOrder(orderId);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
