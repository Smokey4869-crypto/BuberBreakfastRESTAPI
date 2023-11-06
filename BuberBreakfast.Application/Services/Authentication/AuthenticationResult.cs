using BuberBreakfast.Domain.Entities;

namespace BuberBreakfast.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);