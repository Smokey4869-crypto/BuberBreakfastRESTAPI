using Microsoft.AspNetCore.Mvc;
using BuberBreakfast.Contracts.Authentication;
using ErrorOr;
using BuberBreakfast.Domain.Common.Errors;
using MediatR;
using BuberBreakfast.Application.Authentication.Commands.Register;
using BuberBreakfast.Application.Authentication.Queries.Login;
using BuberBreakfast.Application.Authentication.Common;

namespace BuberBreakfast.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // private readonly IAuthenticationCommandService _authenticationCommandService;
    // private readonly IAuthenticationQueryService _authenticationQueryService;

    // public AuthenticationController(
    //     IAuthenticationCommandService authenticationService,
    //     IAuthenticationQueryService authenticationQueryService)
    // {
    //     _authenticationCommandService = authenticationService;
    //     _authenticationQueryService = authenticationQueryService;
    // }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(error)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var authResult = await _mediator.Send(query);

        // Custom response
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(error)
        );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
    }
}