using Application.Features.Authentication.Login;
using Application.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class SigninManager(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : ISigninManager
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
}