using Domain.Models.Requests;
using FluentValidation;

namespace Application.Models.Validations
{
    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId é obrigatório.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("CustomerName é obrigatório.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("ProductName é obrigatório.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount deve ser maior que 0.");
        }
    }
}
