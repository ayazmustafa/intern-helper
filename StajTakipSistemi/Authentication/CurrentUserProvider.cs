using System.Collections.ObjectModel;
using System.Security.Claims;

namespace StajTakipSistemi.Authentication;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public CurrentUser GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            throw new Exception("HttpContext is null");
        }

        var id = GetClaimValues("id")
            .Select(Guid.Parse)
            .First();

        // var permissions = GetClaimValues("permissions");
        var roles = GetClaimValues("role");

        return new CurrentUser(Id: id, Permissions: new List<string>(), Roles: roles);
    }

    private IReadOnlyList<string> GetClaimValues(string claimType)
    {
        return _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();
    }
}
