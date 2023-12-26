using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Common.Behaviours;

public class AuthenticationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private IHttpContextAccessor _httpContextAccessor { get; }
    private ICurrentUserService _currentUserService { get; set; }

    public AuthenticationBehaviour(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService)
    {
        _httpContextAccessor = httpContextAccessor;
        _currentUserService = currentUserService;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrWhiteSpace(token))
        {
        }

        return await next();
    }
}
