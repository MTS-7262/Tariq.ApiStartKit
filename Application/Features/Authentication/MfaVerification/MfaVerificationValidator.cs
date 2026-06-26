using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Authentication.MfaVerification;

public class MfaVerificationValidator : AbstractValidator<MfaVerificationRequest>
{
    public MfaVerificationValidator()
    {
        RuleFor(r => r.Code)
            .NotEmpty().WithMessage("Verification code is required.")
            .Length(6).WithMessage("Verification code must be exactly 6 digits.")
            .Custom((code, context) =>
            {
                var sanitizedCode = code?.Replace(" ", "") ?? "";

                if (sanitizedCode.Length != 6)
                    context.AddFailure("Verification code must be exactly 6 digits.");

                if (!Regex.IsMatch(sanitizedCode, @"^\d+$"))
                    context.AddFailure("Verification code must contain numbers only.");

            });

        RuleFor(r => r.Provider)
            .NotEmpty().WithMessage("Provider is required.")
            .Must(provider => provider is "Authenticator" or "Email")
            .WithMessage("Provider must be either 'Email' or 'SMS'.");
    }
}