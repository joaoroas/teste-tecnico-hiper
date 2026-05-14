namespace Domain.Models.Requests
{
    public class AddOrderRequest
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
    }
}
