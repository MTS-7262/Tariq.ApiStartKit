namespace Application.Features.Authentication.Login;

public record LoginRequest(string Email, string Password, bool RememberMe);
public record LoginResponse(string Token,string RefreshToken,string? Provider="");

public record LoginServiceResponse(bool Mfa, string Provider,string Message);