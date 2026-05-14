using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Models.Response;

namespace Application.UseCases
{
    public class DeleteOrderUseCase : IDeleteOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<string>> DeleteOrderAsync(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return Result<string>.Error("Número do pedido inválido");

                var order = await _orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                    return Result<string>.Error($"Pedido número {orderId} não encontrado");

                await _orderRepository.DeleteOrderByIdAsync(orderId);

                return Result<string>.Ok("Pedido excluido com sucesso");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
