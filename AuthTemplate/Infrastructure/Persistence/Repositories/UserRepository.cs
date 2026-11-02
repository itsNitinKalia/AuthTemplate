using AuthTemplate.Application.Auth.Interfaces;
using AuthTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthTemplate.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AuthDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
