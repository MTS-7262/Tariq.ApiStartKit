namespace Application.Services;

public interface IIdentityDataSeeder
{
    Task SeedAdminUserAsync(CancellationToken cancellationToken = default);
}
