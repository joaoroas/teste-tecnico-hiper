namespace Domain.Models.Requests
{
    public class UpdateOrderRequest : AddOrderRequest
    {
        public  int OrderId { get; set; }
    }
}
