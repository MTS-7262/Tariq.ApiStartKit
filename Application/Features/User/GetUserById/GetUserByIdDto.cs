namespace Application.Features.User.GetUserById;

public record UserResponseDto(Guid Id, string Email, string FirstName, string LastName);
