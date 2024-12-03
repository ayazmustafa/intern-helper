using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using StajTakipSistemi.Authentication;
using StajTakipSistemi.Business.Authentication;
using StajTakipSistemi.Business.AuthenticationBusiness;
using StajTakipSistemi.Controllers.Base;
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Controllers;

[AllowAnonymous]
public class AuthenticationController: ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator, ICurrentUserProvider currentUserProvider)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]User request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var authResult = await _mediator.Send(command);
        // map top response, check bottom
        return Ok(MapToAuthResponse(authResult));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);

        var authResult = await _mediator.Send(query);
        // map top response, check bottom

        return Ok(MapToAuthResponse(authResult));
    }
    

    private static AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }

}