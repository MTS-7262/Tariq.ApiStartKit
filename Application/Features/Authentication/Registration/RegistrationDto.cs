namespace Application.Features.Authentication.Registration;

public sealed record RegisterUserRequest(string FirstName, string LastName, string Email, string Password, string Role);
public sealed record RegisterUserResponse(string Email);