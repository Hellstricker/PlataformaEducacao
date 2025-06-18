using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlataformaEducacao.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;



namespace PlataformaEducacao.WebApps.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                success = false,
                error = context.Exception.Message,
                trace = context.Exception.StackTrace,
            });
            result.StatusCode = StatusCodes.Status500InternalServerError;


            switch (context.Exception)
            {
                case DomainException:
                    result = new ObjectResult(new
                    {
                        success = false,
                        error = context.Exception.Message
                    });
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case ValidationException:
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case UnauthorizedAccessException:
                    result.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                default:
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }


            if(
                context.Exception is DomainException                
                || context.Exception is ValidationException
                || context.Exception is UnauthorizedAccessException
             )
                context.Result = result;
        }
    }
}
