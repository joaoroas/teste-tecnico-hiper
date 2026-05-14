using Application.Extensions;
using Domain.Interfaces.UseCases;
using Domain.Models.Entities;
using Domain.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IAddOrderUseCase _addOrderUseCase;
        private readonly IGetOrderUseCase _getOrderUseCase;
        private readonly IUpdateOrderUseCase _updateOrderUseCase;

        public OrdersController(IAddOrderUseCase addOrderUseCase,
                                IGetOrderUseCase getOrderUseCase,
                                IUpdateOrderUseCase updateOrderUseCase)
        {
            _addOrderUseCase = addOrderUseCase;
            _getOrderUseCase = getOrderUseCase;
            _updateOrderUseCase = updateOrderUseCase;
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
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequest request)
        {
            var validationResult = await _addOrderRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ToCustomValidationFailures());
            }

            var order = new Order(request.CustomerName, request.ProductName, request.Amount);

            await _updateOrderUseCase.UpdateOrderAsync(order);

            return Created("", order);
        }

        //[HttpDelete("Order/{id}")]
        //public async Task<IActionResult> DeleteOrder(int orderId)
        //{
        //    //var validationResult = await _addOrderRequestValidator.ValidateAsync(request);

        //    //if (!validationResult.IsValid)
        //    //{
        //    //    return BadRequest(validationResult.Errors.ToCustomValidationFailures());
        //    //}

        //    //var order = new Order(request.CustomerName, request.ProductName, request.Amount);

        //    //await _addOrderUseCase.AddOrderAsync(order);

        //    return Created();
        //}
    }
}