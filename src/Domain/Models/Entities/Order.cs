using Domain.Enums;

namespace Domain.Models.Entities
{
    public class Order
    {
        private Order() { }
        public Order(string customerName, string product, decimal amount)
        {
            CustomerName = customerName;
            ProductName = product;
            Amount = amount;
            SetOrderStatus(OrderStatus.Received);
            SetCreatedAt(DateTime.UtcNow);
            SetOrderId();
        }
        public int OrderId { get; set; }
        public string CustomerName { get; private set; }
        public string ProductName { get; private set; }
        public decimal Amount { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public void UpdateDetails(string newName, string newProduct, decimal newAmount)
        {

            CustomerName = newName;
            ProductName = newProduct;
            Amount = newAmount;
        }
        private void SetOrderStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }

        private void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        private void SetOrderId()
        {
            OrderId = new Random().Next(1, 1000);
        }
    }
}