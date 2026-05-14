using Application.Extensions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Models.Entities;
using Domain.Models.Requests;
using Domain.Models.Response;
using FluentValidation;

namespace Application.UseCases
{
    public class AddOrderUseCase : IAddOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<AddOrderRequest> _addOrderRequestValidator;


        public AddOrderUseCase(IOrderRepository orderRepository, 
                               IValidator<AddOrderRequest> addOrderRequestValidator)
        {
            _orderRepository = orderRepository;
            _addOrderRequestValidator = addOrderRequestValidator;
        }

        public async Task<Result<Order>> AddOrderAsync(AddOrderRequest request)
        {
            try
            {
                var validationResult = await _addOrderRequestValidator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.ToCustomValidationFailures();

                    return new Result<Order>(false, errors);
                }
                
                var order = new Order(request.CustomerName, request.ProductName, request.Amount);

                await _orderRepository.AddOrderAsync(order);

                return Result<Order>.Ok("Pedido criado com sucesso", order);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
