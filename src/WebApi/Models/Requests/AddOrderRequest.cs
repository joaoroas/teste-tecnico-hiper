using Domain.Enums;

namespace WebApi.Models.Requests
{
    public class AddOrderRequest
    {
        public string CustumerName { get; set; }
        public string Product { get; set; }
        public decimal Value { get; set; }
    }
}
