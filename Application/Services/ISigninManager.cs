using Application.Features.Authentication.Login;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public interface ISigninManager
{
    Task<SignInResult> PasswordSignInAsync(LoginRequest request);
    Task<LoginServiceResponse> RequiredTwoFactorAsync();
}