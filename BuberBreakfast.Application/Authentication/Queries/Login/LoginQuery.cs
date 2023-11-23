using BuberBreakfast.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberBreakfast.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;