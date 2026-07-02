namespace Application.Features.User.GetAllUsers;

public record GetPaginatedUsersQuery(int PageNumber = 1, int PageSize = 10);

public record UserResponseDto(string Id, string Email, string FirstName, string LastName);


public class Users
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
