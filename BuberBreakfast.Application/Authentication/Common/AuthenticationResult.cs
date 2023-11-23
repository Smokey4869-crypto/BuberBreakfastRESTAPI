using BuberBreakfast.Domain.Entities;

namespace BuberBreakfast.Application.Authentication.Common;

public record AuthenticationResult(
    User User, 
    string Token
);