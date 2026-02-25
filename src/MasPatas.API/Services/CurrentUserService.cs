using System.Security.Claims;
using MasPatas.Application.Interfaces;

namespace MasPatas.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId =>
        Guid.Parse(_httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new InvalidOperationException("UserId not found in token"));

    public string? Username =>
        _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.Name)?.Value;
}
