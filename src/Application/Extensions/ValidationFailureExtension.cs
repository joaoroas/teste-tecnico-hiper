using Domain.Models.Error;
using FluentValidation.Results;

namespace Application.Extensions
{
    public static class ValidationFailureExtension
    {
        public static IList<CustomValidationFailure> ToCustomValidationFailures(this IList<ValidationFailure> failures)
        {
            return failures.Select(error => new CustomValidationFailure(error.PropertyName, error.ErrorMessage)).ToList();
        }
    }
}
    