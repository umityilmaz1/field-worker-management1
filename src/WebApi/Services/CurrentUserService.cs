using System.Security.Claims;

using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get { 
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(a => a.Type == "UserId")?.Value);
            return userId > 0 ? userId : null;
        }
    }

    public string? Token
    {
        get
        {
            var token = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(a => a.Type == "Token")?.Value;
            return !string.IsNullOrWhiteSpace(token) ? token : null;
        }
    }
}
