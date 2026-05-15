using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Models.Entities;
using Domain.Models.Response;

namespace Application.UseCases
{
    public class GetOrderUseCase : IGetOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<Order>>> GetOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetOrdersAsync();

                if (orders == null || !orders.Any())
                    return Result<List<Order>>.Ok("Nenhum pedido encontrado");

                return Result<List<Order>>.Ok(responseData: orders);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Result<Order>> GetOrderByIdAsync(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return Result<Order>.Error("Número do pedido inválido");
                
                var order = await _orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                    return Result<Order>.Error($"Pedido número {orderId} não encontrado");
                
                return Result<Order>.Ok(responseData: order);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
