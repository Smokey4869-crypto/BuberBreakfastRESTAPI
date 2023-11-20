using BuberBreakfast.Application.Common.Interfaces.Authentication;
using BuberBreakfast.Application.Common.Interfaces.Persistance;
using BuberBreakfast.Domain.Common.Errors;
using BuberBreakfast.Domain.Entities;
using ErrorOr;

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

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password) 
    {
        // Check if user already exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
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

    public ErrorOr<AuthenticationResult> Login(string email, string password) 
    {
        // Validate if user already exists
        if (_userRepository.GetUserByEmail(email) is not User user) 
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // Validate the password is correct
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );  
    }
}