using System.Net.Mime;
using System.Security.Authentication;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using CleanArchitecture.Model.Commons;

namespace WebApi.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ICurrentUserService currentUserService, ISender sender)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;

                var returnData = ReturnData<string>.Fail();

                if (ex.GetType() == typeof(ValidationException))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    returnData.Message = "Validasyon hatası";

                    foreach (var item in (ex as ValidationException).Errors)
                    {
                        returnData.Message += Environment.NewLine + item.Value.FirstOrDefault() ?? string.Empty;
                    }
                }
                else if (ex.GetType() == typeof(NotFoundException))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    returnData.Message = ex.Message;
                }
                else if (ex.GetType() == typeof(UnauthorizedAccessException))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    returnData.Message = "Not Authenticated";
                }
                else if (ex.GetType() == typeof(AuthenticationException))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    returnData.Message = "Not Authenticated";
                }
                else if (ex.GetType() == typeof(UnexpectedException))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    returnData.Message = ex.Message;
                }
                else if (ex.GetType() == typeof(ConflictException))
                {
                    var baseException = (BaseException)ex;
                    httpContext.Response.StatusCode = (int)baseException.StatusCode;
                    returnData.Message = ex.Message;
                    returnData.SetMessageReceiver(baseException.Receiver);
                }
                else if (ex.GetType() == typeof(PaycellException))
                {
                    var baseException = (BaseException)ex;
                    httpContext.Response.StatusCode = (int)baseException.StatusCode;
                    returnData.Message = ex.Message;
                    returnData.SetMessageReceiver(baseException.Receiver);
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    returnData.Message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        returnData.Message += $"\n\n Inner Exception : {ex.InnerException.Message}";
                    }
                }


                var aaa = currentUserService.UserId;
                //await sender.Send(new AddErrorLogCommand { UserId = aaa, Exception = ex });
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(returnData));
            }
        }
    }

    public static class GlobalExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}