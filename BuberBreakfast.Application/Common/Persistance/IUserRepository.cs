using BuberBreakfast.Domain.Entities;

namespace BuberBreakfast.Application.Common.Interfaces.Persistance;

public interface IUserRepository 
{
    User? GetUserByEmail(string email);
    void Add(User user);   
}