namespace Application.Features.Authentication.MfaDisable;

public record MfaDisableRequest(string Password);
public record MfaDisableResponse(bool IsSuccess, string Message);