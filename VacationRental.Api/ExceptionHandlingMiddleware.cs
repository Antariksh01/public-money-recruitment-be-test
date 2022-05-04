using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VacationRental.Domain.Exception;

namespace VacationRental.Api
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                  await next(context);
            }
            catch(ValidationException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(e.Message);
            }
            catch(NotFoundException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(e.Message);
            }
        }
    }
}
