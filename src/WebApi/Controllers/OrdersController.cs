using Domain.Entities;
using Domain.Interfaces.UseCases;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Models.Requests;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IAddOrderUseCase _addOrderUseCase;
        private readonly IValidator<AddOrderRequest> _addOrderRequestValidator;

        public OrdersController(IAddOrderUseCase addOrderUseCase,
                                IValidator<AddOrderRequest> addOrderRequestValidator)
        {
            _addOrderUseCase = addOrderUseCase;
            _addOrderRequestValidator = addOrderRequestValidator;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderRequest request)
        {
            var validationResult = await _addOrderRequestValidator.ValidateAsync(request);

            if ( !validationResult.IsValid )
            {
                return BadRequest(validationResult.Errors.ToCustomValidationFailures());
            }

            var order = new Order(request.CustumerName, request.Product, request.Value);

            await _addOrderUseCase.AddOrderAsync(order);

            return Created("", order);
        }
    }
}