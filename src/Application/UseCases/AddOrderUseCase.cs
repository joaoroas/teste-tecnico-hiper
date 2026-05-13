using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;

namespace Application.UseCases
{
    public class AddOrderUseCase : IAddOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }
    }
}
