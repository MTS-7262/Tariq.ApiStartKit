using Application.Abstractions;
using Domain.Abstractions;

namespace Application.Features.User.GetAllUsers;

public class GetUsersHandler : IHandler<GetPaginatedUsersQuery, Result<PaginatedList<UserResponseDto>>>
{
    public async Task<Result<PaginatedList<UserResponseDto>>> HandleAsync(GetPaginatedUsersQuery command, CancellationToken cancellationToken)
    {
        var pageNumber = command.PageNumber < 1 ? 1 : command.PageNumber;
        var pageSize = command.PageSize < 1 ? 10 : (command.PageSize > 100 ? 100 : command.PageSize);

        var query = userRepository.Query();

        // 2. Fetch overall total count for metadata calculations
        int totalCount = await query.CountAsync(cancellationToken);

        // 3. Process database pagination math segment mapping (Skip / Take)
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
