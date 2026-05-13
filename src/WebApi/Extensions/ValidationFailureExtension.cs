using FluentValidation.Results;
using WebApi.Models.Error;

namespace WebApi.Extensions
{
    public static class ValidationFailureExtension
    {
        public static IList<CustomValidationFailure> ToCustomValidationFailures(this IList<ValidationFailure> failures)
        {
            return failures.Select(error => new CustomValidationFailure(error.PropertyName, error.ErrorMessage)).ToList();
        }
    }
}
    