using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.GetAllUsers;

public class GetUsersHandler(IUserManager userManager) : IHandler<GetPaginatedUsersQuery, Result<PaginatedList<UserResponseDto>>>
{
    public async Task<Result<PaginatedList<UserResponseDto>>> HandleAsync(GetPaginatedUsersQuery command, CancellationToken cancellationToken)
    {
        var pageNumber = command.PageNumber < 1 ? 1 : command.PageNumber;
        var pageSize = command.PageSize < 1 ? 10 : (command.PageSize > 100 ? 100 : command.PageSize);

        var query = userManager.Query();

        var totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .OrderBy(u => u.Email)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(user => new UserResponseDto(
                user.Id,
                user.Email!,
                user.FirstName,
                user.LastName
            ))
            .ToListAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var paginatedResult = new PaginatedList<UserResponseDto>(users, pageNumber, totalPages, totalCount);

        return Result.Success(paginatedResult);
    }
}
