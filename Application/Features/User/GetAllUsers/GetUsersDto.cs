namespace Application.Features.User.GetAllUsers;

public record GetPaginatedUsersQuery(int PageNumber = 1, int PageSize = 10);

public record UserResponseDto(string Id, string Email, string FirstName, string LastName);
