namespace Application.Features.Authentication.MfaSetup;

public record ToggleMfaSetupRequest(string Provider, bool Enable, string? VerificationCode = null);
public record ToggleMfaSetupResponse(bool IsSuccess, string Message);
public record MfaSetupResponse(string SharedKey, string QrCodeUrl);