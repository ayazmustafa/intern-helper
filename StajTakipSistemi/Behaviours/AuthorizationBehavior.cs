using System.Reflection;
using MediatR;
using StajTakipSistemi.Authentication;
using StajTakipSistemi.Exceptions;
using AuthorizeAttribute = StajTakipSistemi.Authentication.AuthorizeAttribute;

namespace StajTakipSistemi.Behaviours;

public class AuthorizationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserProvider _currentUserProvider;

    public AuthorizationBehavior(ICurrentUserProvider currentUserProvider)
    {
        _currentUserProvider = currentUserProvider;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizationAttributes = request.GetType()
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();

        if (authorizationAttributes.Count == 0)
        {
            return await next();
        }

        var currentUser = _currentUserProvider.GetCurrentUser();

        var requiredPermissions = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Permissions?.Split(',') ?? [])
            .ToList();

        if (requiredPermissions.Except(currentUser.Permissions).Any())
        {
            throw new UnauthroizedException("User is forbidden from taking this action");
        }

        var requiredRoles = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Roles?.Split(',') ?? [])
            .ToList();

        if (requiredRoles.Except(currentUser.Roles).Any())
        {
            throw new UnauthroizedException("User is forbidden from taking this action");
        }

        return await next();
    }
}