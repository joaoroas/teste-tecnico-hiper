using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public Order(string custumerName, string product, decimal value)
        {
            CustumerName = custumerName;
            Product = product;
            Value = value;
        }
        public string CustumerName { get; private set; }
        public string Product { get; private set; }
        public decimal Value { get; private set; }
        public OrderStatus Status { get; private set; }
    }
}