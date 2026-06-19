using Domain.Abstractions.Errors;

namespace Application.Features.Authentication;

public static class AuthenticationErrors
{
    public static Error UserExit(string email) =>
        Error.Conflict("User.AlreadyExist", $"{email} already exist!");
}