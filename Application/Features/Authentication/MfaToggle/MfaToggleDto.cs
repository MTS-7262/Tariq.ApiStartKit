namespace Application.Features.Authentication.MfaToggle;

public record MfaToggleRequest(string Provider, bool Enable, string? VerificationCode = null);
public record MfaToggleResponse(bool IsSuccess, string Message);