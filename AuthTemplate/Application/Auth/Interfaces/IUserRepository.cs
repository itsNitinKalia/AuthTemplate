using AuthTemplate.Application.Common.Interfaces;
using AuthTemplate.Domain.Entities;

namespace AuthTemplate.Application.Auth.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
