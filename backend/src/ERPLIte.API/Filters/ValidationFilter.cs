using ERPLite.Application.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERPLite.API.Filters;

public sealed class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(
    ActionExecutingContext context,
    ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            var validatorType =
                typeof(IValidator<>)
                    .MakeGenericType(argument.GetType());

            var validator =
                _serviceProvider.GetService(validatorType);

            if (validator is not IValidator fluentValidator)
                continue;

            var validationContext =
                new ValidationContext<object>(argument); 

            var result =
                await fluentValidator.ValidateAsync(
                    validationContext,
                    context.HttpContext.RequestAborted);

            if (!result.IsValid)
            {
                throw new ERPLite.Application.Common.Exceptions.ValidationException(
                    result.Errors.Select(x => x.ErrorMessage));
            }
        }

        await next();
    }
}