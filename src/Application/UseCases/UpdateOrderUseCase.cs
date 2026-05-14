using Application.Extensions;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Models.Requests;
using Domain.Models.Response;
using FluentValidation;

namespace Application.UseCases
{
    public class UpdateOrderUseCase : IUpdateOrderUseCase
    {
        private readonly IValidator<UpdateOrderRequest> _validator;
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderUseCase(IValidator<UpdateOrderRequest> validator, IOrderRepository orderRepository)
        {
            _validator = validator;
            _orderRepository = orderRepository;
        }

        public async Task<Result<string>> UpdateOrderAsync(UpdateOrderRequest updateRequest)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(updateRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.ToCustomValidationFailures();
                    return new Result<string>(false, errors);
                }

                var order = await _orderRepository.GetOrderByIdAsync(updateRequest.OrderId);

                if (order == null)
                    return Result<string>.Error($"Pedido número {updateRequest.OrderId} não encontrado");


                if(order.OrderStatus != OrderStatus.Received)
                    return Result<string>.Error($"Pedido já em processamento, não pode ser atualizado.");


                order.UpdateDetails(updateRequest.CustomerName, updateRequest.ProductName, updateRequest.Amount);

                await _orderRepository.UpdateOrderAsync(order);

                return Result<string>.Ok($"Pedido {order.OrderId} atualizado com sucesso");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
