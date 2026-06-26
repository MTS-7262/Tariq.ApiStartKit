using Application.Features.Authentication.Login;
using Application.Features.Authentication.MfaSetup;
using Application.Features.Authentication.MfaToggle;
using Application.Features.Authentication.MfaVerification;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public interface ISigninManager
{
    Task<SignInResult> PasswordSignInAsync(LoginRequest request);
    Task<LoginServiceResponse> RequiredTwoFactorAsync();
    Task<MfaVerificationResponse> TwoFactorSignInAsync(MfaVerificationRequest request);
    Task<MfaSetupResponse> GetAuthenticatorSetupDetailsAsync();
    Task<MfaToggleResponse> ToggleMfaAsync(MfaToggleRequest request);
}