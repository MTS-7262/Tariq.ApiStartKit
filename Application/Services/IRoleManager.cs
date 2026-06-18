namespace Application.Services;

public interface IRoleManager
{
    Task<bool> RoleExistAsync(string roleName);
}