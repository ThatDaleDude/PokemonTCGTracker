using API.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CollectedCard> CollectedCards { get; init; }
    public DbSet<FavouriteSet> FavouriteSets { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CollectedCard>(builder =>
        {
            builder.HasIndex(x => new { x.SetId, x.PokedexId }).IsUnique();
            builder.Property(x => x.SetId).HasMaxLength(255).IsRequired();
        });

        modelBuilder.Entity<FavouriteSet>(builder =>
        {
            builder.HasIndex(x => x.SetId);
            builder.Property(x => x.SetId).HasMaxLength(255).IsRequired();
        });
    }
}