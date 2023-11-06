using BuberBreakfast.Domain.Entities;

namespace BuberBreakfast.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}