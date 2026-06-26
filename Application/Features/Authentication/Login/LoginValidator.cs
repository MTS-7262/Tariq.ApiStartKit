using FluentValidation;

namespace Application.Features.Authentication.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email Address is Required")
            .EmailAddress().WithMessage("Invalid Email Address");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is Required")
            .MinimumLength(6).WithMessage("Password should have at least 6 characters");
    }
}