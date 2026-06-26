namespace Application.Features.Authentication.MfaSetup;

public record MfaSetupResponse(string SharedKey, string QrCodeUrl);