using MasPatas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasPatas.Infrastructure.Persistence;

public class MasPatasDbContext(DbContextOptions<MasPatasDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MasPatasDbContext).Assembly);
    }
}
