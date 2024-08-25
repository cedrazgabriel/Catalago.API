using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Catalago.API.Midlewares;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            // Pegar o tipo do body utilizando reflexão
            var bodyType = GetBodyTypeFromRequest(context);
            if (bodyType != null)
            {
                // Ler o body da request
                context.Request.EnableBuffering();
                var body = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                // Deserializar o corpo da requisição para o tipo identificado
                var bodyObject = System.Text.Json.JsonSerializer.Deserialize(body, bodyType);

                // Validar utilizando FluentValidation
                var validatorType = typeof(IValidator<>).MakeGenericType(bodyType);
                var validator = serviceProvider.GetService(validatorType) as IValidator;
                if (validator != null)
                {
                    ValidationResult result = await validator.ValidateAsync(new ValidationContext<object>(bodyObject));
                    if (!result.IsValid)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                        });
                        return;
                    }
                }
            }
        }

        await next(context);
    }

    private Type GetBodyTypeFromRequest(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (actionDescriptor != null)
            {
                // Procurar o parâmetro que está associado ao body da request
                var bodyParameter = actionDescriptor.Parameters
                    .FirstOrDefault(p => p.BindingInfo?.BindingSource == Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource.Body);

                if (bodyParameter != null)
                {
                    return bodyParameter.ParameterType;
                }
            }
        }

        return null;
    }
}