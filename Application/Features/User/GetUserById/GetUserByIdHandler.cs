using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;

namespace Application.Features.User.GetUserById;

internal class GetUserByIdHandler(IUserManager userManager) : IHandler<Guid, Result<UserResponseDto>>
{
    public async Task<Result<UserResponseDto>> HandleAsync(Guid command, CancellationToken cancellationToken)
    {
        var result = await userManager.GetUserByIdAsync(command);
        var user = new UserResponseDto(result.Id, result.Email, result.FirstName, result.LastName);

        return Result.Success(user);
    }
}
