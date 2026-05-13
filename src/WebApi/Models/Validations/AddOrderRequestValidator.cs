using FluentValidation;
using WebApi.Models.Requests;

namespace WebApi.Models.Validations
{
    public class AddOrderRequestValidator : AbstractValidator<AddOrderRequest>
    {
        public AddOrderRequestValidator()
        {
            RuleFor(x => x.CustumerName)
                .NotEmpty().WithMessage("Customer name is required.");

            RuleFor(x => x.Product)
                .NotEmpty().WithMessage("Product is required.");

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Value must be greater than 0.");
        }
    }
}
