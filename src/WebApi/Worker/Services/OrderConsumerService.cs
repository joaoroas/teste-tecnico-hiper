using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Models.Messaging;
using WebApi.Worker.Interfaces;

namespace WebApi.Worker.Services
{
    public class OrderConsumerService : IOrderConsumerService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderConsumerService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task ProcessOrderAsync(OrderMessage orderMessage)
        {
            try
            {
                await Task.Delay(30000);

                await _orderRepository.UpdateOrderStatusAsync(orderMessage.OrderId, OrderStatus.Processing);

                await Task.Delay(30000);

                await _orderRepository.UpdateOrderStatusAsync(orderMessage.OrderId, OrderStatus.Completed);

            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
