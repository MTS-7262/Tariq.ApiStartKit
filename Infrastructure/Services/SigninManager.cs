using Application.Features.Authentication.Login;
using Application.Features.Authentication.MfaSetup;
using Application.Features.Authentication.MfaVerification;
using Application.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class SigninManager(SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration) : ISigninManager
{
    public async Task<SignInResult> PasswordSignInAsync(LoginRequest request)
    {
        var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, true);
        return result;
    }

    public async Task<LoginServiceResponse> RequiredTwoFactorAsync()
    {
        var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
            throw new ArgumentException("Error processing security context.");

        var validProviders = await userManager.GetValidTwoFactorProvidersAsync(user);

        // CASE A: User has Time-based App MFA active
        if (validProviders.Contains(TokenOptions.DefaultAuthenticatorProvider))
            return new LoginServiceResponse(true, "Authenticator", "Please enter the 6-digit code from your Authenticator app.");

        // CASE B: User has Email MFA active
        if (!validProviders.Contains(TokenOptions.DefaultEmailProvider))
            throw new ArgumentException("Invalid credentials.");


        var emailToken = await userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

        // TODO: Trigger your custom notification system / Domain Event here!
        // Example: _mediator.Publish(new MfaEmailTokenGeneratedEvent(user.Email, emailToken));
        //_logger.LogInformation($"[MFA EVENT TRIGGERED] Code {emailToken} sent to {user.Email}");

        return new LoginServiceResponse(true, "Email", "A security code has been dispatched to your registered email address.");

    }

    public async Task<MfaVerificationResponse> TwoFactorSignInAsync(MfaVerificationRequest request)
    {
        var actualProvider = request.Provider == "Authenticator"
            ? TokenOptions.DefaultAuthenticatorProvider
            : TokenOptions.DefaultEmailProvider;

        var result = await signInManager.TwoFactorSignInAsync(
            actualProvider,
            request.Code.Replace(" ", ""),
            isPersistent: request.RememberMe,
            rememberClient: false
        );

        if (result.Succeeded)
            return new MfaVerificationResponse("Token", "RefreshToken");

        if (result.IsLockedOut)
            throw new ArgumentException("Account locked.");

        throw new ArgumentException("Invalid verification code provided.");
    }

    public async Task<MfaSetupResponse> GetAuthenticatorSetupDetailsAsync()
    {
        var principal = httpContextAccessor.HttpContext?.User;
        if (principal == null) throw new ArgumentException("User session not found.");

        var user = await userManager.GetUserAsync(principal);
        if (user == null) throw new ArgumentException("User not found.");

        var unformattedKey = await userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(unformattedKey))
        {
            await userManager.ResetAuthenticatorKeyAsync(user);
            unformattedKey = await userManager.GetAuthenticatorKeyAsync(user);
        }

        var appName = configuration["ApplicationName"] ?? string.Empty;
        if (string.IsNullOrWhiteSpace(appName))
            throw new ArgumentException("Application Name Not Found!");

        var qrCodeUri = $"otpauth://totp/{Uri.EscapeDataString(appName)}:{Uri.EscapeDataString(user.Email!)}?secret={unformattedKey}&issuer={Uri.EscapeDataString(appName)}&digits=6";

        return new MfaSetupResponse(unformattedKey, qrCodeUri);

    }

    public Task<ToggleMfaSetupResponse> ToggleMfaAsync(ToggleMfaSetupRequest request)
    {
        throw new NotImplementedException();
    }
}