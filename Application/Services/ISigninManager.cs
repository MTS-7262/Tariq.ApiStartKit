using Application.Features.Authentication.Login;
using Application.Features.Authentication.Mfa;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public interface ISigninManager
{
    Task<SignInResult> PasswordSignInAsync(LoginRequest request);
    Task<LoginServiceResponse> RequiredTwoFactorAsync();
    Task<MfaVerificationResponse> TwoFactorSignInAsync(MfaVerificationRequest request);
}