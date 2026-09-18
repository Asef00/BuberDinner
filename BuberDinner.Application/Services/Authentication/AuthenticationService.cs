using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        return Task.FromResult(new AuthenticationResult(
            Guid.NewGuid(), "John", "Doe", email, _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), "John", "Doe")));
    }

    public Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        // check if user already exists

        // create user (generate unique ID)

        // create JWT token
        var token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), firstName, lastName);

        Guid userId = Guid.NewGuid();

        return Task.FromResult(new AuthenticationResult(userId, firstName, lastName, email, token));
    }
}