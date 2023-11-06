using BuberBreakfast.Application.Common.Interfaces.Authentication;
using BuberBreakfast.Application.Common.Interfaces.Persistance;
using BuberBreakfast.Domain.Entities;

namespace BuberBreakfast.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password) 
    {
        // Check if user already exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User with given email already exists");
        }

        // Create user (generate unique ID)
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName, 
            Email = email, 
            Password = password
        };

        _userRepository.Add(user);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }

    public AuthenticationResult Login(string email, string password) 
    {
        // Validate if user already exists
        if (_userRepository.GetUserByEmail(email) is not User user) 
        {
            throw new Exception("User with given email does not exist");
        }

        // Validate the password is correct
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );  
    }
}