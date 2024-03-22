using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using VerticalSliceArchitecture.Entities;

namespace VerticalSliceArchitecture.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; set; }

    public DbSet<Hero> Heroes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_Article_Id");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(e => e.Content)
                .IsRequired();

            //Check for more information: https://learn.microsoft.com/en-us/ef/core/modeling/value-comparers?tabs=ef5#mutable-classes
            entity.Property(e => e.Tags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default)!,
                    new ValueComparer<List<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

            entity.Property(e => e.CreatedOnUtc)
                .IsRequired()
                .HasDefaultValueSql("getutcdate()");

            entity.Property(e => e.PublishedOnUtc)
                .IsRequired(false);
        });

        modelBuilder.Entity<Hero>(entity =>
        {
            entity.ToTable(nameof(Hero), "dbo");

            entity.HasKey(e => e.Id)
                .HasName("PK_Hero_Id");

            entity.Property(e => e.Name)
                .HasColumnName(nameof(Hero.Name))
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.Power)
                .HasColumnName(nameof(Hero.Power))
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.IsAlive)
                .HasColumnName(nameof(Hero.IsAlive))
                .HasDefaultValueSql("0")
                .IsRequired();
        });
    }
}
