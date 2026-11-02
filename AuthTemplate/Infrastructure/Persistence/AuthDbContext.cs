using AuthTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthTemplate.Infrastructure.Persistence;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(200);
            entity.Property(x => x.PasswordHash).IsRequired();
            entity.Property(x => x.Role).IsRequired().HasMaxLength(50);
            entity.HasIndex(x => x.Email).IsUnique();
        });
    }
}
