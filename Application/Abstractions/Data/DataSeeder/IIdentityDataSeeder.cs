using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data.DataSeeder;

public interface IIdentityDataSeeder
{
    Task SeedAdminUserAsync(CancellationToken cancellationToken = default);
}
