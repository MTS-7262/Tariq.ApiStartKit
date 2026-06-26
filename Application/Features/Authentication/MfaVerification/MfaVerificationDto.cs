namespace Application.Features.Authentication.MfaVerification;

public record MfaVerificationRequest(string Provider, string Code, bool RememberMe);
public record MfaVerificationResponse(string Token, string RefreshToken);