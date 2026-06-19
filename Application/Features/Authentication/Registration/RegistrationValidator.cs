using FluentValidation;

namespace Application.Features.Authentication.Registration;

public class RegistrationValidator : AbstractValidator<RegisterUserRequest>
{
    public RegistrationValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First Name is Required")
            .MinimumLength(3).WithMessage("First Name should at least 3 characters")
            .MaximumLength(100).WithMessage("First Name must not exceed 100 characters");

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last Name is Required")
            .MinimumLength(3).WithMessage("Last Name should at least 3 characters")
            .MaximumLength(100).WithMessage("Last Name must not exceed 100 characters");

        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("Invalid Email Address");

        RuleFor(c => c.Password)
            .MinimumLength(6).WithMessage("Password should have at least 6 characters");
    }

}