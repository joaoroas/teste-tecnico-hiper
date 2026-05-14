using Domain.Interfaces.UseCases;
using Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IAddOrderUseCase _addOrderUseCase;
        private readonly IGetOrderUseCase _getOrderUseCase;
        private readonly IUpdateOrderUseCase _updateOrderUseCase;
        private readonly IDeleteOrderUseCase _deleteOrderUseCase;

        public OrdersController(IAddOrderUseCase addOrderUseCase,
                                IGetOrderUseCase getOrderUseCase,
                                IUpdateOrderUseCase updateOrderUseCase,
                                IDeleteOrderUseCase deleteOrderUseCase)
        {
            _addOrderUseCase = addOrderUseCase;
            _getOrderUseCase = getOrderUseCase;
            _updateOrderUseCase = updateOrderUseCase;
            _deleteOrderUseCase = deleteOrderUseCase;
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _getOrderUseCase.GetOrdersAsync();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("Order/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var result = await _getOrderUseCase.GetOrderByIdAsync(orderId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> AddOrder(AddOrderRequest request)
        {
            var result = await _addOrderUseCase.AddOrderAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Created("", result);
        }

        [HttpPut("Order")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequest updatRequest)
        {
            var result = await _updateOrderUseCase.UpdateOrderAsync(updatRequest);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Order/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _deleteOrderUseCase.DeleteOrderAsync(orderId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}