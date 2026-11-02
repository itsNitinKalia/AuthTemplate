using AuthTemplate.Application.Common.Interfaces;
using AuthTemplate.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthTemplate.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity>(AuthDbContext dbContext) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly AuthDbContext DbContext = dbContext;
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
